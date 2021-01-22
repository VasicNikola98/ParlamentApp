using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParlamentApp.Entities
{
    public class SessionManager
    {
        private static IDriver _driver = null;
        private static ISession _session = null;

        public static IDriver GetDriver()
        {
            if (_driver == null)
            {
                _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "skupstina"));

                return _driver;
            }


            return _driver;
        }

        public static ISession GetSession()
        {
            if(_session==null)
            {
                _session = GetDriver().Session();
            }

            return _session;
        }
    }
}
