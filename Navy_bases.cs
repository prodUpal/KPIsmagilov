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
    
    public partial class Navy_bases
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Navy_bases()
        {
            this.Units = new HashSet<Units>();
        }
    
        public int IDBase { get; set; }
        public string NavalBaseName { get; set; }
        public string GeographicalLocation { get; set; }
        public int CountOfUnits { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Units> Units { get; set; }
    }
}