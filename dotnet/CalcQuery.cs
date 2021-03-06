using GraphQL.Types;
using System;
using System.Diagnostics;
using System.Linq;
using ConsoleApp1;

public class CalcQuery : ObjectGraphType
{
    public static string SigningKey { get; set; }
    public static bool IsDevMode { get; set; }

    public CalcQuery()
    {
        Field<StringGraphType>(
            name: "awake",
            resolve: context => "Awake"
        );

        Field<StringGraphType>(
            name: "exit",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "signingkey" }
            ),
            resolve: context => {
                var signingkey = context.GetArgument<string>("signingkey");
                if (signingkey != SigningKey)
                {
                    return "invalid signature";
                }
                Environment.Exit(0);
                return "exit";
            }
        );

        Field<StringGraphType>(
            name: "hello",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "signingkey" }
            ),
            resolve: context => {
                var signingkey = context.GetArgument<string>("signingkey");
                if (signingkey != SigningKey)
                {
                    return "invalid signature";
                }
                return "world";
            }
        );

        Field<StringGraphType>(
            name: "calc",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "signingkey" },
                //new QueryArgument<StringGraphType> { Name = "math" },
                new QueryArgument<StringGraphType> { Name = "a" },
                new QueryArgument<StringGraphType> { Name = "b" },
                new QueryArgument<StringGraphType> { Name = "populationAmount" },
                new QueryArgument<StringGraphType> { Name = "numberOfBits" },
                new QueryArgument<StringGraphType> { Name = "epochsAmount" },
                new QueryArgument<StringGraphType> { Name = "bestAndTournamentChomosomeAmount" },
                new QueryArgument<StringGraphType> { Name = "eliteStrategyAmount" },
                new QueryArgument<StringGraphType> { Name = "crossProbability" },
                new QueryArgument<StringGraphType> { Name = "mutationProbability" },
                new QueryArgument<StringGraphType> { Name = "inversionProbability" },
                new QueryArgument<StringGraphType> { Name = "selectionMethod" }, 
                new QueryArgument<StringGraphType> { Name = "crossMethod" }, 
                new QueryArgument<StringGraphType> { Name = "mutationMethod" }, 
                new QueryArgument<StringGraphType> { Name = "maximization" }
            ),
            resolve: context => {
                var signingkey = context.GetArgument<string>("signingkey");
                if (signingkey != SigningKey)
                {
                    return "invalid signature";
                }
                //var math = context.GetArgument<string>("math");
                var a = int.Parse(context.GetArgument<string>("a"));
                var b = int.Parse(context.GetArgument<string>("b"));
                var populationAmount = int.Parse(context.GetArgument<string>("populationAmount"));
                var numberOfBits = int.Parse(context.GetArgument<string>("numberOfBits"));
                var epochsAmount = int.Parse(context.GetArgument<string>("epochsAmount"));
                var bestAndTournamentChomosomeAmount = double.Parse(context.GetArgument<string>("bestAndTournamentChomosomeAmount"));
                var eliteStrategyAmount = double.Parse(context.GetArgument<string>("eliteStrategyAmount"));
                var crossProbability = double.Parse(context.GetArgument<string>("crossProbability"));
                var mutationProbability = double.Parse(context.GetArgument<string>("mutationProbability"));
                var inversionProbability = double.Parse(context.GetArgument<string>("inversionProbability"));
                var selectionMethod = context.GetArgument<string>("selectionMethod");
                var crossMethod = context.GetArgument<string>("crossMethod");
                var mutationMethod = context.GetArgument<string>("mutationMethod");
                var maximization = context.GetArgument<string>("maximization").Equals("maksymalizacja");

                MutationMethod muatationEnum = MutationMethod.ONE_POINT;
                switch (mutationMethod)
                {
                    case "jednopunktowa":
                        muatationEnum = MutationMethod.ONE_POINT;
                        break;
                    case "dwupunktowa":
                        muatationEnum = MutationMethod.TWO_POINT;
                        break;
                    case "brzegowa":
                        muatationEnum = MutationMethod.BRZEGOWA;
                        break;
                }

                SelectionMethod selectionEnum = SelectionMethod.BEST;
                switch (selectionMethod)
                {
                    case "best":
                        selectionEnum = SelectionMethod.BEST;
                        break;
                    case "kolo":
                        selectionEnum = SelectionMethod.Kolo_Ruletki;
                        break;
                    case "turniej":
                        selectionEnum = SelectionMethod.Turniejowa;
                        break;
                }

                CrossMethod crossEnum = CrossMethod.ONE_POINT;
                switch (crossMethod)
                {
                    case "jednopunktowe":
                        crossEnum = CrossMethod.ONE_POINT;
                        break;
                    case "dwupunktowe":
                        crossEnum = CrossMethod.TWO_POINT;
                        break;
                    case "jednorodne":
                        crossEnum = CrossMethod.JEDNORODNE;
                        break;
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var strategy = new Strategy(new IndividualGenerator(), new SelekcjaNajlepszych(), new SelekcjaTurniejowa(), new SelekcjaRuletka());
                var res = strategy.Execute(a, b, populationAmount, numberOfBits, epochsAmount,
                    bestAndTournamentChomosomeAmount, eliteStrategyAmount, crossProbability, mutationProbability,
                    inversionProbability, selectionEnum, crossEnum, muatationEnum, maximization);
                stopwatch.Stop();
                Individual winner;
                double x1;
                double x2;
                int y;

                if (maximization)
                {
                    winner = res.OrderByDescending(x => x.FunctionResult).First();
                    x1 = winner.X1;
                    x2 = winner.X2;
                }
                else
                {
                    winner = res.OrderBy(x => x.FunctionResult).First();
                    x1 = winner.X1;
                    x2 = winner.X2;
                }


                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds,
                    stopwatch.Elapsed.Milliseconds / 10);
                //var result = Calc.Eval(math);
                var result = $"Wynik: ({x1}, {x2}) = {winner.FunctionResult}. Czas: {elapsedTime}";
                return Convert.ToString(result);
            }
        );
    }

   
}
