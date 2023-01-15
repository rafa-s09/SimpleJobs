namespace SimpleJobs.Brazil.Documents;

/// <summary>
/// Contains validation functions for Brazilian documents. <br/>
/// <i>Note: There is no eligibility check, it just checks if the number is a valid value</i>
/// </summary>
public static class BrazilValidations
{
    /// <summary>
    /// This method validates the document by the verification digit
    /// </summary>
    /// <param name="cpf">CPF document number as string</param>
    /// <returns>BrazilValidationResult [Success if it valid]</returns>
    public static BrazilValidationResult CheckForCPF(string cpf)
    {
        // Is null is failed
        if (string.IsNullOrEmpty(cpf.Replace(" ", "")))
            return BrazilValidationResult.Failed;

        cpf = cpf.ClearSymbols();

        if (cpf.Length != 11)
            return BrazilValidationResult.WrongSize;

        int[] firstDigit = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] secondDigit = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string temp, digit;
        int sum, rest;        

        temp = cpf[..9];
        sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(temp[i].ToString()) * firstDigit[i];

        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = rest.ToString();
        temp += digit;
        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += int.Parse(temp[i].ToString()) * secondDigit[i];

        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit += rest.ToString();

        if (cpf.EndsWith(digit))
            return BrazilValidationResult.Success;
        else
            return BrazilValidationResult.Failed;
    }

    /// <summary>
    /// This method validates the document by the verification digit
    /// </summary>
    /// <param name="cnpj">CNPJ document number as string</param>
    /// <returns>BrazilValidationResult [Success if it valid]</returns>
    public static BrazilValidationResult CheckForCNPJ(string cnpj) 
    {
        // Is null is failed
        if (string.IsNullOrEmpty(cnpj.Replace(" ", "")))
            return BrazilValidationResult.Failed;       

        cnpj = cnpj.ClearSymbols();

        if (cnpj.Length != 14)
            return BrazilValidationResult.WrongSize;

        // After validation variables can be declared
        int[] firstDigit = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] secondDigit = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum, rest;
        string digit, temp;

        temp = cnpj[..12];
        sum = 0;

        for (int i = 0; i < 12; i++)
            sum += int.Parse(temp[i].ToString()) * firstDigit[i];

        rest = (sum % 11);
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = rest.ToString();
        temp += digit;
        sum = 0;

        for (int i = 0; i < 13; i++)
            sum += int.Parse(temp[i].ToString()) * secondDigit[i];

        rest = (sum % 11);
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit += rest.ToString();

        if (cnpj.EndsWith(digit))
            return BrazilValidationResult.Success;
        else
            return BrazilValidationResult.Failed;
    }

    /// <summary>
    /// This method validates the document by the verification digit
    /// </summary>
    /// <param name="pis">Pis document number as string</param>
    /// <returns>BrazilValidationResult [Success if it valid]</returns>
    public static BrazilValidationResult CheckForPIS(string pis)
    {
        // Is null is failed
        if (string.IsNullOrEmpty(pis.Replace(" ", "")))
            return BrazilValidationResult.Failed;

        pis = pis.ClearSymbols();

        if (pis.Length != 11)
            return BrazilValidationResult.WrongSize;

        int[] validDigit = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum, rest;        

        pis = pis.Trim().Replace("-", "").Replace(".", "").PadLeft(11, '0');
        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += int.Parse(pis[i].ToString()) * validDigit[i];

        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        if (pis.EndsWith(rest.ToString()))
            return BrazilValidationResult.Success;
        else
            return BrazilValidationResult.Failed;
    }
}

