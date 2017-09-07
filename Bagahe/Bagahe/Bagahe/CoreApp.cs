using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bagahe
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();


            this.RegisterAppStart<MainViewModel>();

        }
    }
}
