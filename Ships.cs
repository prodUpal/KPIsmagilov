//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KPIsmagilov
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ships
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ships()
        {
            this.Teachings = new HashSet<Teachings>();
        }
    
        public int ID_ship { get; set; }
        public string ShipName { get; set; }
        public string TypeOfShip { get; set; }
        public int DateCreation { get; set; }
        public int OperatingTime { get; set; }
        public int NumberOfSeats { get; set; }
        public string EngineDevice { get; set; }
        public string DriveType { get; set; }
        public string HousingPlacement { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Teachings> Teachings { get; set; }
    }
}
