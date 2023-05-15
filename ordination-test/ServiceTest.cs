namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

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
/*
    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(4, service.GetDagligFaste().Count());
    }
    */


    [TestMethod]
    public void doegnDosisTest()
    {
        DagligFast Test1 = new DagligFast( new DateTime(2023, 01, 01), new DateTime(2023, 01, 07), new Laegemiddel("Methotrexat", 0.1, 0.15, 0.16, "Styk"), 2, 2, 1, 0);


        double doegnDosisTestTestCase1 = Test1.doegnDosis();

        Assert.AreEqual(5, Test1.doegnDosis());

        

    }

    [TestMethod]
    public void  

/*
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.

        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.
        //Kald noget kode der smider en expetion, eventuelt lav en new med negative værdier
        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
    */
} 