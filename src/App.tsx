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

    const handleKeyDown = (event:React.KeyboardEvent<HTMLInputElement>) => {
        if (event.key === "Enter") {
            const math = event.currentTarget.value;
            if (appGlobalClient === null) {
                setMathResult("this page only works when hosted in electron");
                return;
            }
            appGlobalClient.query({
                query:gql`query calc($signingkey:String!, $math:String!) {
                    calc(signingkey:$signingkey, math:$math)
                }`,
                variables: {
                    math,
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
    };

    return (
        <div className="App">
            <header className="App-header">
                <img src={logo} className="App-logo" alt="logo"/>
             <h1>Booth function</h1>
                <input
                    style={{ color:"black" }}
                    onKeyDown={handleKeyDown}
                />

    <form>
        <label>
            Minimalny X (parametr a):
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Maksymalny X (parametr b):
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Liczebnosc populacji:
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Liczba bitow:
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Liczba epok:
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Liczba chromossom w selekcji najlepszych i turniejowej:
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Procent osobnikow do strategii elitarnej (ulamek):
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Prawdopodobienstwo krzyzowania:
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Prawdopodobienstwo mutacji:
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Prawdopodobienstwo inwersji:
        <input type="text" name="parametera" />
        </label>
        <br />
        <label>
            Metoda selekcji
        <select>
            <option value="best">Selekcja najlepszych</option>
            <option value="kolo">Selekcja 'kolo ruletki'</option>
            <option selected value="turniej">Selekcja turniejowa</option>
        </select>
        </label>
        <br />
        <label>
            Metoda krzyzowania
        <select>
            <option value="jednopunktowe">Krzyzowanie jednopunktowe</option>
            <option value="dwupunktowe">Krzyzowanie dwupunktowe</option>
            <option selected value="jednorodne">Krzyzowanie jednorodne</option>
        </select>
        </label>
        <br />
        <label>
            Metoda Mutacji
        <select>
            <option value="jednopunktowa">Mutacja jednopunktowa</option>
            <option value="dwupunktowa">Mutacja dwupunktowa</option>
            <option selected value="brzegowa">Mutacja brzegowa</option>
        </select>
        </label>
        <br />
        <input type="submit" value="Start" />
       
    </form>
    
                <div>
                    {mathResult}
                </div>
            </header>
        </div>
    );
};

export default App;
