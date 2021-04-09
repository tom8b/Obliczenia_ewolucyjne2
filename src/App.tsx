import { InMemoryCache } from "apollo-cache-inmemory";
import { ApolloClient } from "apollo-client";
import { HttpLink } from "apollo-link-http";
import gql from "graphql-tag";
import fetch from "isomorphic-fetch";
import React, { useMemo, useState } from "react";
import "./App.css";
import logo from "./logo.svg";

const ipcRenderer = (window as any).isInElectronRenderer
        ? (window as any).nodeRequire("electron").ipcRenderer
        : (window as any).ipcRendererStub;

const App = () => {
    const [mathResult, setMathResult] = useState("");
    const [apiPort, setApiPort] = useState(0);
    const [apiSigningKey, setApiSigningKey] = useState("");

    const appGlobalClient = useMemo(() => {
        if (apiPort === 0) {
            if (ipcRenderer) {
                ipcRenderer.on("apiDetails", ({}, argString:string) => {
                    const arg:{ port:number, signingKey:string } = JSON.parse(argString);
                    setApiPort(arg.port); // setting apiPort causes useMemo'd appGlobalClient to be re-evaluated
                    setApiSigningKey(arg.signingKey);
                });
                ipcRenderer.send("getApiDetails");
            }
            return null;
        }
        return new ApolloClient({
            cache: new InMemoryCache(),
            link: new HttpLink({
                fetch:(fetch as any),
                uri: "http://127.0.0.1:" + apiPort + "/graphql/",
            }),
        });
    }, [apiPort]);


    const handleSubmit = (event: any) => {
        event.preventDefault();
      

        if (appGlobalClient === null) {
            setMathResult("this page only works when hosted in electron");
            return;
        }

        appGlobalClient.query({
            query:gql`query calc($signingkey:String!, $a:String!, $b:String!, $populationAmount:String!, $numberOfBits:String!
                , $epochsAmount:String!, $bestAndTournamentChomosomeAmount:String!, $eliteStrategyAmount:String!, $crossProbability:String!,
                 $mutationProbability:String!, $inversionProbability:String!, $selectionMethod:String!, $crossMethod:String!, $mutationMethod:String!, $maximization:String!) {
                calc(signingkey:$signingkey, a:$a, b:$b, populationAmount:$populationAmount, numberOfBits:$numberOfBits, epochsAmount:$epochsAmount,
                    bestAndTournamentChomosomeAmount:$bestAndTournamentChomosomeAmount, eliteStrategyAmount:$eliteStrategyAmount, crossProbability:$crossProbability,
                    mutationProbability:$mutationProbability, inversionProbability:$inversionProbability, selectionMethod:$selectionMethod, crossMethod:$crossMethod, mutationMethod:$mutationMethod, maximization:$maximization)
            }`,
            variables: {
                a: event.target[0].value,
                b: event.target[1].value,
                populationAmount: event.target[2].value,
                numberOfBits: event.target[3].value,
                epochsAmount: event.target[4].value,
                bestAndTournamentChomosomeAmount: event.target[5].value,
                eliteStrategyAmount: event.target[6].value,
                crossProbability: event.target[7].value,
                mutationProbability: event.target[8].value,
                inversionProbability: event.target[9].value,
                selectionMethod: event.target[10].value,
                crossMethod: event.target[11].value,
                mutationMethod: event.target[12].value,
                maximization: event.target[13].value,
                signingkey: apiSigningKey,
            },
        })
        .then(({ data }) => {
            setMathResult(data.calc);
        })
        .catch((e) => {
            console.log("Error contacting graphql server");
            console.log(e);
            setMathResult("Error getting result with port=" + apiPort + " and signingkey='" + apiSigningKey + "' (if this is the first call, the server may need a few seconds to initialize)");
        });




      }

    return (
        <div className="App">
            <header className="App-header">
                <img src={logo} className="App-logo" alt="logo"/>
             <h1>Booth function</h1>

    <form onSubmit={handleSubmit}>
        <label>
            Minimalny X (parametr a):
        <input type="text" name="minx" />
        </label>
        <br />
        <label>
            Maksymalny X (parametr b):
        <input type="text" name="maxx" />
        </label>
        <br />
        <label>
            Liczebnosc populacji:
        <input type="text" name="liczebnosc" />
        </label>
        <br />
        <label>
            Liczba bitow:
        <input type="text" name="liczbabitow" />
        </label>
        <br />
        <label>
            Liczba epok:
        <input type="text" name="liczbaepok" />
        </label>
        <br />
        <label>
            Liczba chromossom w selekcji najlepszych i turniejowej:
        <input type="text" name="liczbachromosomow" />
        </label>
        <br />
        <label>
            Procent osobnikow do strategii elitarnej (ulamek):
        <input type="text" name="procentosobnikowdoelitranej" />
        </label>
        <br />
        <label>
            Prawdopodobienstwo krzyzowania:
        <input type="text" name="prawdopodobienstwokrzyzowania" />
        </label>
        <br />
        <label>
            Prawdopodobienstwo mutacji:
        <input type="text" name="prawdopodobiesntwomutacji" />
        </label>
        <br />
        <label>
            Prawdopodobienstwo inwersji:
        <input type="text" name="prawdopodobienstwoinwersji" />
        </label>
        <br />
        <label>
            Metoda selekcji
        <select>
            <option value="best">Selekcja najlepszych</option>
            <option value="kolo">Selekcja 'kolo ruletki'</option>
            <option value="turniej">Selekcja turniejowa</option>
        </select>
        </label>
        <br />
        <label>
            Metoda krzyzowania
        <select>
            <option value="jednopunktowe">Krzyzowanie jednopunktowe</option>
            <option value="dwupunktowe">Krzyzowanie dwupunktowe</option>
            <option value="jednorodne">Krzyzowanie jednorodne</option>
        </select>
        </label>
        <br />
        <label>
            Metoda Mutacji
        <select>
            <option value="jednopunktowa">Mutacja jednopunktowa</option>
            <option value="dwupunktowa">Mutacja dwupunktowa</option>
            <option  value="brzegowa">Mutacja brzegowa</option>
        </select>
        </label>
        <br />
        <label>
            Optimum
        <select>
            <option value="maksymalizacja">Maksymalizacja</option>
            <option value="minimalizacja">Minimializacja</option>
        </select>
        </label>
        <br />
        <button type="submit">Start</button>
    
    </form>
    
                <div>
                    {mathResult}
                </div>
            </header>
        </div>
    );
};

export default App;
