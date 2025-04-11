using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Castle.SomeCode
{
    internal class ClassMB
    {
        public static void ErrorMB(Exception exception)
        {
            MessageBox.Show(exception.Message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }



        public static void ErrorMB(string text)
        {
            MessageBox.Show(text, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }


        public static void ErrorMB(DbEntityValidationException ex)
        {
            foreach (DbEntityValidationResult validationError in
                        ex.EntityValidationErrors)
            {
                foreach (DbValidationError err in validationError
                    .ValidationErrors)
                {
                    InformationMB(err.ErrorMessage + " \n" + "Object: " + validationError
                    .Entry.Entity.ToString());
                }
            }
        }

        public static void InformationMB(string text)
        {
            MessageBox.Show(text, "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static bool Question(string question)
        {
            return MessageBoxResult.Yes == MessageBox.Show(question,
                "Вопрос", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
        }

    }
}
