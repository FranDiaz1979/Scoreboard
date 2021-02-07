Technical Test with only a library made with KISS, TDD and Clean Code and a simple console app for deal with the library 
(without data storage, webapi, etc.)

The entire description of the requirements is in the .pdf file

Mis notas:
TDD por delante

DDD:
Hay 2 Domain Models: Score y ScoreBoard
ScoreBoard es una lista de Resultados deportivos (scores), se podía haber quitado este modelo para hacer más facil el codigo,
pero me gusta aplicar la lógica de DDD al crear los modelos y por eso lo he hecho así.


CheckOnlyOneGoal
    // La diferencia en score de home==0 y away==1 o bien score de home==1 y away==0 o peta
    //      Se permite que el update sea 1 gol negativo por si el cliente se equivoca, que pueda rectificar
    //      Se permite que la diferencia sea 0 por si se envia n veces la misma peticion


