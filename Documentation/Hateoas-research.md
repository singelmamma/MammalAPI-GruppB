# Hateoas

Research och implementation enligt Hateoas:
https://www.e4developer.com/2018/02/16/hateoas-simple-explanation/

Hateoas är en typ av hyperlänkar i ett json-response på olika metoder. Enligt ovanstående källa, kan man implementera dessa länkar, men tyvärr refererar artikeln och dess länkar endast till java.

## Research

Det finns många  spår att gå på, men tre av dem verkar mer lovande än andra. Två av dem hyfsat enkla att förstå och implementera, varav den tredje lite mer omfattande.

1. Implementing HATEOAS in your asp.netcore webapi...

   * https://medium.com/@ylerjen/implementing-hateoas-in-your-asp-netcore-webapi-2139df4e7b0c

   * **Beskrivning:** Denna metod verkar väldigt lätt, dock använder den sig av asp.netcore2. Kan dock gå att få implementation att fungera. Manuell länkbuildning som troligen kräver att vi får lägga till en hel del DTOer (även om de visar samma data) pga att man skräddarsyr länkarna mot getmetoderna & controllersarna inuti dem. Denna tutorial använder sig också av automapper.

2. Nuget-paket asp.netcore2

   - https://github.com/faniereynders/aspnetcore-hateoas
   - https://www.nuget.org/packages/AspNetCore.Hateoas/
   - https://reynders.co/a-dead-simple-hateoas-provider-for-asp-net-core/

   * **Beskrivning:** Nuget med dependency av newtonsoft.json. En extension på mvc. Lätt att implementera och installera till den grad att det inte riktigt var helt enkelt att första hur header accept och request skulle läggas till.  Fick något fel när jag försökte, och kom in på någon sida ang. att .netcore tagit bort newtonsoft.json i asp.netcore3.

     Kanske går det om man trixar lite mer.

3. Implementing HATEOAS in ASP.NET Core Web API

   * https://code-maze.com/hateoas-aspnet-core-web-api/
* **Beskrivning:** Använder sig av .netcore3. Väldigt omfattande, men som sagt. asp.netcore3.

4. Adding HATEOAS to an ASP.NET CORE API
   * https://baldbeardedbuilder.com/posts/adding-hateoas-to-an-asp-net-core-api/
   * Beskrivning: Verkar vara en av de bättre tutorialsen. Lyckats komma ända tills anrop där jag får en nullreferens-error. Vet ännu inte hur jag skall lösa denna.

