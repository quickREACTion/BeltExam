using System;
using System.ComponentModel.DataAnnotations;


namespace BeltExam
{
    public class AcivitiesValidator
    {   
        [Required(ErrorMessage= "Title is a required field!")]
        [MinLength(2, ErrorMessage="Title requires a minimum of 2 letters!")]
        public string Title { get; set; }

        [Required(ErrorMessage= "Date is a required field!")]
        [currenDateAttribute(ErrorMessage = "Entered Date already passed!")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage= "Time is a required field!")]
        // [currentTimeAttribute(ErrorMessage = "The time is already passed!")]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage= "Duration is a required field!")]
        public TimeSpan? Duration { get; set; }

        [Required(ErrorMessage= "Description is a required field!")]
        [MinLength(10 , ErrorMessage="Description requires a minimum of 10 letters!")]
        public string Description { get; set; }

    }

    public class currenDateAttribute : ValidationAttribute {
        public currenDateAttribute() {

        }
        public override bool IsValid(object value) {
            if(value != null) {
            var dt = (DateTime)value;
                if (dt >= DateTime.Now) {
                    return true;
                }
                return false;
            }
            return false;
        }
    }


    public class currentTimeAttribute : ValidationAttribute {
        public currentTimeAttribute() {

        }
        public override bool IsValid(object value) {
            if(value != null) {
            var ct = (TimeSpan)value;
                if (ct >= TimeSpan.MinValue) {
                    return true;
                }
                return false;
            }
            return false;
        }
    }





}