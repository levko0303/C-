using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using WebApplication1.Models2;

namespace WebApplication1
{
    class Validation
    {
        public static bool Id_Check(int ContractId)
        {
            if (ContractId is int)
            {
                return true;
            }
            return false;
        }


        public static bool Name_Check(string name)
        {
            if (Regex.IsMatch(name, @"^[a-zA-Z]+$") && name.Length > 2 && name.Length < 40)
            {
                return true;
            }
            return false;
        }

        

        public static bool Email_Check(string ContrctEmail)
        {
            if (ContrctEmail.EndsWith("@gmail.com"))
            {
                return true;
            }
            return false;
        }

        public static bool Phone_Check(string ContractPhone)
        {
            if (ContractPhone.Length == 13 )
            {
                return true;
            }
            return false;
        }

        

        public static bool IBAN_Check(string ContractIBAN)
        {
            if (ContractIBAN.Length ==13)
            {
                return true;
            }
            return false;
        }

        

        public static bool isValid(Contract contract, out JsonResult j)
        {
            string message = "";
            bool res = true;
            
            if (!Id_Check(contract.ContractId))
            {
                message += "Id must be int. ";
                res = false;
            }
            if (!Name_Check(contract.ContractName))
            {
                message += "Name is incorrect. ";
                res = false;
            }
            if (!Email_Check(contract.ContractEmail))
            {
                message += "must be ****@gmail.com. ";
                res = false;
            }
            if (!Phone_Check(contract.ContractPhone))
            {
                message += "incorrect phone number ";
                res = false;
            }
            if (!IBAN_Check(contract.ContractPhone))
            {
                message += "incorrect IBAN ";
                res = false;
            }

            j = new JsonResult(message);
            return res;
        }
    }
}
