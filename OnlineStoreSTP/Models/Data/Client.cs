//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineStoreSTP.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> ManagerId { get; set; }
    
        public virtual Manager Manager { get; set; }
        public virtual Product Product { get; set; }
        public virtual ClientStatus ClientStatus { get; set; }
    }
}
