using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTMovie.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieLib;

namespace RESTMovie.Managers.Tests
{
    [TestClass()]
    public class MoviesManagerTests
    {
        /// <summary>
        /// ASSERT: Er der en collection af hjælper klasser
        /// til at teste conditions i unit test, og hvis 
        /// denne condition ikke bliver genkendt, kastes der en exception
        /// </summary>
        
        //initialisere et objekt, ref til manager,
        //så hver enkelt metode ikke skal initialisere et
        //nyt objekt for hver gang
        private MoviesManager _manager = new MoviesManager();

        //Get all har ingen parametre
        //vi tester på de mock objekter der er i listen
        [TestMethod()]
        public void GetAllTest()
        {
            //is true - tester på om den specifikke 
            //condition er true
            //og kaster en exception hvis denne condition
            //er false
            Assert.IsTrue(_manager.GetAll().Count() == 3);
        }

        [TestMethod()]
        public void GetFilterTest()
        {
            //tester om filteret på LengthInMinutes virker
            Assert.IsTrue(_manager.GetFilter(155).Count() == 2);
            //Assert.IsTrue(_manager.GetFilter(133).Count() == 3);
            //Assert.IsTrue(_manager.GetFilter(155).Count() == 1);
            //tester på et filter der ikke returnere noget, 
            //forventer at der er ikke er noget
            Assert.IsTrue(_manager.GetFilter(0).Count() == 0);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            //tjekker den 1 movie
            Assert.IsTrue(_manager.GetById(1).Name.Equals("Dune"));
            //tjekker et id der ikke burde eksistere
            //tester på om objektet er null, hvis den ikke er null
            //kastes der en exception
            Assert.IsNull(_manager.GetById(100));
        }

        //test Add metoden og delete
        [TestMethod()]
        public void AddMovieTest()
        {
            //læser count før den tilføjer en ny movie
            //så vi kan sammenligne den til det nummer 
            //efter at den er tilføjet
            //bagefter rydder vi op, så andre metoder ikke bliver
            //påvirket af ændringerne
            int beforeAddingACount = _manager.GetAll().Count();
            //opretter en var, med et id som vi tildeler den,
            //den burde blive overskrevet når manager tilføjer en movie
            int defaultId = 0;
            //opretter et test movie der skal tilføjes
            Movie newMovie = new Movie(defaultId, "TestMovie", 200, "Finland");
            //tilføjer denne movie, og opbevarer resultatet i en variabel
            Movie addTheResult = _manager.AddMovie(newMovie);
            //og opbevarer det nye tildelte id
            int newId = addTheResult.Id;
            //tjekker om manageren assigner det nye id
            //tester om valuen ikke er ens, kaster derfor en ex hvis de er ens
            Assert.AreNotEqual(defaultId, newId);
            //derefter tjekker vi på om Count af listen, er 1
            //mere end den var før
            Assert.AreEqual(beforeAddingACount + 1, _manager.GetAll().Count());
            
            //tjekker om id'et af det slettet movie er det samme
            //som som vi bedte den om at slette
            Movie deletedMovie = _manager.DeleteMovie(newId);
            //tjekker om count nu er det samme som FØR vi begyndte
            //at tilføje og slette
            Assert.AreEqual(beforeAddingACount, _manager.GetAll().Count());
            //og til sidst tjekker vi på om hvis vi vil slette et id
            //der ikke eksitere, at den så returnere null
            Assert.IsNull(_manager.DeleteMovie(50));
        }
    }
}