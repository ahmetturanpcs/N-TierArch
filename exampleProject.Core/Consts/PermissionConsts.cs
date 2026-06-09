using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exampleProject.Core.Consts
{
    // BAŞINDAKİ 'public' ANAHTARINA DİKKAT ET
    public static class PermissionConsts
    {
        // BURADA DA 'public' OLMALI
        public static class Category
        {
            public const string View = "Permissions.Category.View";
            public const string Create = "Permissions.Category.Create";
            public const string Update = "Permissions.Category.Update";
            public const string Delete = "Permissions.Category.Delete";
        }

        // BURADA DA 'public' OLMALI
        public static class Product
        {
            public const string View = "Permissions.Product.View";
            public const string Create = "Permissions.Product.Create";
            public const string Update = "Permissions.Product.Update";
            public const string Delete = "Permissions.Product.Delete";
        }
    }
}
