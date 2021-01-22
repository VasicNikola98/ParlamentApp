using Microsoft.Owin;
using Neo4j.Driver;
using Neo4jClient;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(ParlamentApp.Startup))]
namespace ParlamentApp
{
    public partial class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
          
        }
    }
}
