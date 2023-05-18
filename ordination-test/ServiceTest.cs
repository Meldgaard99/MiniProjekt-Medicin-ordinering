namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;
using shared;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }





    [TestMethod]
    public void samletDosisTest()
    {

        //Tester om metoden samletDosis() virker som den skal
        //Tester gyldig data
        //Testen er lavet med 2 forskellige scenarier, hvor der testes for 49 og 100 dagelige doser



        DagligFast samletDosisTest1 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 07), new Laegemiddel("Methotrexat", 0.1, 0.15, 0.16, "Styk"), 4, 2, 1, 0);

        double DagligFastTestCase1 = samletDosisTest1.samletDosis();

        Assert.AreEqual(49, DagligFastTestCase1);




        DagligFast samletDosisTest2 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 10), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), 5, 0, 5, 0);


        double DagligFastTestCase2 = samletDosisTest2.samletDosis();

         Assert.AreEqual(100, DagligFastTestCase2);

    }



    [TestMethod]
    public void samletDosisTestFejl()
    {

        //Tester om metoden samletDosis() virker som den skal
        //Tester gyldig data
        //Testen er lavet med 2 forskellige scenarier, hvor der testes for 49 og 100 dagelige doser



        DagligFast samletDosisTest3 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 07), new Laegemiddel("Methotrexat", 0.1, 0.15, 0.16, "Styk"), -4, -2, -1, 0);

        double DagligFastTestCase3 = samletDosisTest3.samletDosis();

        Assert.AreEqual(-49, DagligFastTestCase3);




        DagligFast samletDosisTest4 = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 10), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), -5, 0, -5, 0);


        double samletDosisTestCase4 = samletDosisTest4.samletDosis();

         Assert.AreEqual(-100, samletDosisTestCase4);

    }


    [TestMethod]
    public void doegnDosisTester()
    {

        //Tester gyldig data
        //Tester metoden doegnDosis i klassen DagligFast
        //Testen er lavet med 2 forskellige scenarier, hvor der testes for 7 og 13 dagelige doser
        DagligFast doegnDosisTest1 = new DagligFast(new DateTime(2023, 05, 20), new DateTime(2023, 05, 25), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), 4, 2, 1, 0);


        double doegnDosisTestCase1 = doegnDosisTest1.doegnDosis();


        Assert.AreEqual(7, doegnDosisTestCase1);




        DagligFast doegnDosisTest2 = new DagligFast(new DateTime(2023, 05, 20), new DateTime(2023, 09, 12), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), 10, 2, 1, 0);

    
        double doegnDosisTestCase2 = doegnDosisTest2.doegnDosis();


        Assert.AreEqual(13, doegnDosisTestCase2);
    }


     [TestMethod]
    public void doegnDosisTesterFejl()
    {

        //Tester ugyldig data
        //Tester metoden doegnDosis i klassen DagligFast
        //Testen er lavet med 2 forskellige scenarier, hvor der testes for -7 og -13 dage
        DagligFast doegnDosisTest1 = new DagligFast(new DateTime(2023, 05, 20), new DateTime(2023, 05, 25), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), -4, -2, -1, 0);


        double doegnDosisTestCase1 = doegnDosisTest1.doegnDosis();


        Assert.AreEqual(-7, doegnDosisTestCase1);



        DagligFast doegnDosisTest2 = new DagligFast(new DateTime(2023, 05, 20), new DateTime(2023, 09, 12), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), -10, -2, -1, 0);

    
        double doegnDosisTestCase2 = doegnDosisTest2.doegnDosis();


        Assert.AreEqual(-13, doegnDosisTestCase2);
    }


    [TestMethod]
    public void AntalDageTest()
    {
        //Tester gyldig data
        //Tester metoden antalDage i klassen DagligFast
        //Testen er lavet med 3 forskellige scenarier, hvor der testes for 3, 10 og 30 dage
        //Det er gyldig værdier

        DagligFast antalDagekort = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 04), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), 4, 2, 1, 0);

        int antalDageTestCase1 = antalDagekort.antalDage();

        Assert.AreEqual(4, antalDageTestCase1);


        DagligFast antalDageMellem = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 10), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), 4, 2, 1, 0);


        int antalDageTestCase2 = antalDageMellem.antalDage();
        Assert.AreEqual(10, antalDageTestCase2);


        DagligFast antalDageLang = new DagligFast(new DateTime(2023, 01, 01), new DateTime(2023, 01, 30), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), 4, 2, 1, 0);
        int antalDageTestCase3 = antalDageLang.antalDage();
        Assert.AreEqual(30, antalDageTestCase3);



    }

     [TestMethod]
    public void AntalDageTestFejl()
    { 
        //Tester ugyldig datoer, hvor startdaten er højere end slutdatoen 
        //Testen er lavet med 2 forskellige scenarier, hvor der testes for 1 og 10 dage
        //Det er ugyldig værdier
        //Den vil altid returnere -1, da det er ugyldig værdier og det et defineret i klassen 
        DagligFast antalDagekort = new DagligFast(new DateTime(2023, 01, 02), new DateTime(2023, 01, 01), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), 4, 2, 1, 0);

        int antalDageTestCase4 = antalDagekort.antalDage();

        Assert.AreEqual(-1, antalDageTestCase4);


        DagligFast antalDageMellem = new DagligFast(new DateTime(2023, 01, 10), new DateTime(2023, 01, 01), new Laegemiddel("Pencilin", 0.1, 0.15, 0.16, "Styk"), 4, 2, 1, 0);

        int antalDageTestCase5 = antalDageMellem.antalDage();

        Assert.AreEqual(-1, antalDageTestCase5);

    }


     [TestMethod]
    public void GivDosisTest()
    {
        //Tester gyldig data
        //tester metoden givDosis i klassen PN
        PN GivDosisTest1 = new PN(new DateTime(2023, 01, 01), new DateTime(2023, 01, 12), 123, new Laegemiddel("Fucidin", 0.025, 0.025, 0.025, "Styk"));

        bool givDosisTestCase1 = GivDosisTest1.givDosis(new Dato { dato = new DateTime(2023, 01, 02).Date });

        Assert.AreEqual(true, givDosisTestCase1);

    }


      [TestMethod]
    public void GivDosisTestFejl()
    {
        //Tester ugyldig data
        //tester metoden givDosis i klassen PN
        PN GivDosisTest2 = new PN(new DateTime(2023, 01, 01), new DateTime(2023, 01, 12), 123, new Laegemiddel("Fucidin", 0.025, 0.025, 0.025, "Styk"));

        bool givDosisTestCase2 = GivDosisTest2.givDosis(new Dato { dato = new DateTime(2023, 01, 13).Date });

        Assert.AreEqual(false, givDosisTestCase2);

    }

/*
//Virker ikke endnu :(, arbejder stærkt på sagen
[TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.

        // Hvis koden ikke smider en exception,
        // så fejler testen.
        //Kald noget kode der smider en expetion, eventuelt lav en new med negative værdier
        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
    */

/*


[TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnExceptions()
    { 
    

        if ( Assert.AreEqual(-1, antalDageTestCase4)
        {
            throw new ArgumentNullException("some Method received a null argument!");
        }
        //throw new ArgumentNullException("some Method received a null argument!");
        
    }
    
*/

}