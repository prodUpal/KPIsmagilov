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
    using System.Linq;
    using System.Windows.Input;

    public partial class Personnel
    {
        public int ID_serviceman { get; set; }
        public int ID_Units { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
        public System.DateTime YearOfBirth { get; set; }
        public System.DateTime YearOfEntryIntoService { get; set; }
        public int LengthOfService { get; set; }
        public string Awards { get; set; }
        public string Photo { get; set; }
    
        public virtual Units Units { get; set; }


        public string PersonnelAwardsDisplay
        {
            get
            {
                var awards = KPIsmagilovEntities.GetContext().Personnel.ToString();

                if (Awards == null)
                    return "Отсутствуют";

                return Awards.ToString();
            }
            set
            {

            }
        }

        public string PersonnelPositionDisplay
        {
            get
            {
                var awards = KPIsmagilovEntities.GetContext().Personnel.ToString();


                return Position.ToString();
            }
            set
            {

            }
        }

        public string YearOfBirthDisplay 
        {
            get
            {
                var current = KPIsmagilovEntities.GetContext().Personnel.ToList();


                
                return YearOfBirth.Year.ToString() + "." + YearOfBirth.Month.ToString() + "." + YearOfBirth.Day.ToString();
            }
        }

        public string YearOfEntryIntoServiceDisplay
        {
            get
            {
                var current = KPIsmagilovEntities.GetContext().Personnel.ToList();



                return YearOfEntryIntoService.Year.ToString() + "." + YearOfEntryIntoService.Month.ToString() + "." + YearOfEntryIntoService.Day.ToString();
            }
        }

    }
}
