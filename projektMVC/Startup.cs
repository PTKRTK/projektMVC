using Microsoft.Owin;
using Owin;
using projektMVC.Models;
using System;
using System.Timers;

[assembly: OwinStartupAttribute(typeof(projektMVC.Startup))]
namespace projektMVC
{
    public partial class Startup
    {
        


        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
        }

        

        
    }
}
