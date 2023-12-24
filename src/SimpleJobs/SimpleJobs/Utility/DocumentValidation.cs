namespace SimpleJobs.Utility;

/// <summary>
/// Valida documentos do territorio brasileiro
/// </summary>
public static partial class DocumentValidation
{
    #region Regex

    [GeneratedRegex(@"^\d{11}$")]
    private static partial Regex CPFRegex();

    [GeneratedRegex(@"^\d{14}$")]
    private static partial Regex CNPJRegex();

    [GeneratedRegex(@"^\d{11}$")]
    private static partial Regex PISRegex();

    #endregion Regex

    #region Validadores

    /// <summary>
    /// Valida um CPF.
    /// </summary>
    /// <param name="cpf">O CPF a ser validado.</param>
    /// <returns>Enumerador correspondente DocumentValidationResponse</returns>
    public static DocumentValidationResponse CPFIsValid(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
            return DocumentValidationResponse.Invalid;

        cpf = cpf.ClearSymbols();
        if (cpf.Length != 11 || !CPFRegex().IsMatch(cpf))
            return DocumentValidationResponse.WrongSize;

        int[] multiplicadoresPrimeiroDigito = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicadoresSegundoDigito = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
        string digitoTemporario, digitoFinal;
        int somaDigito, resto;

        digitoTemporario = cpf[..9];
        somaDigito = 0;

        for (int i = 0; i < 9; i++)
            somaDigito += int.Parse(digitoTemporario[i].ToString()) * multiplicadoresPrimeiroDigito[i];

        resto = somaDigito % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digitoFinal = resto.ToString();
        digitoTemporario += digitoFinal;
        somaDigito = 0;

        for (int i = 0; i < 10; i++)
            somaDigito += int.Parse(digitoTemporario[i].ToString()) * multiplicadoresSegundoDigito[i];

        resto = somaDigito % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digitoFinal += resto.ToString();
        return cpf.EndsWith(digitoFinal) ? DocumentValidationResponse.Valid : DocumentValidationResponse.Invalid;

    }

    /// <summary>
    /// Valida um CNPJ.
    /// </summary>
    /// <param name="cnpj">O CNPJ a ser validado.</param>
    /// <returns>Enumerador correspondente DocumentValidationResponse</returns>
    public static DocumentValidationResponse CNPJIsValid(string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj))
            return DocumentValidationResponse.Invalid;

        cnpj = cnpj.ClearSymbols();
        if (cnpj.Length != 14 || !CNPJRegex().IsMatch(cnpj))
            return DocumentValidationResponse.WrongSize;

        int[] multiplicadoresPrimeiroDigito = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicadoresSegundoDigito = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        string digitoFinal, digitoTemporario;
        int somaDigito, resto;

        digitoTemporario = cnpj[..12];
        somaDigito = 0;

        for (int i = 0; i < 12; i++)
            somaDigito += int.Parse(digitoTemporario[i].ToString()) * multiplicadoresPrimeiroDigito[i];

        resto = (somaDigito % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digitoFinal = resto.ToString();
        digitoTemporario += digitoFinal;
        somaDigito = 0;

        for (int i = 0; i < 13; i++)
            somaDigito += int.Parse(digitoTemporario[i].ToString()) * multiplicadoresSegundoDigito[i];

        resto = (somaDigito % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digitoFinal += resto.ToString();
        return cnpj.EndsWith(digitoFinal) ? DocumentValidationResponse.Valid : DocumentValidationResponse.Invalid;
    }

    /// <summary>
    /// Valida um PIS.
    /// </summary>
    /// <param name="pis">O PIS a ser validado.</param>
    /// <returns>Enumerador correspondente DocumentValidationResponse</returns>
    public static DocumentValidationResponse PISsValid(string pis)
    {
        if (string.IsNullOrEmpty(pis))
            return DocumentValidationResponse.Invalid;

        pis = pis.ClearSymbols();
        if (pis.Length != 11 || !PISRegex().IsMatch(pis))
            return DocumentValidationResponse.WrongSize;

        int[] multiplicadores = [3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int somaDigito, resto;

        pis = pis.Trim().Replace("-", "").Replace(".", "").PadLeft(11, '0');
        somaDigito = 0;

        for (int i = 0; i < 10; i++)
            somaDigito += int.Parse(pis[i].ToString()) * multiplicadores[i];

        resto = somaDigito % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        return pis.EndsWith(resto.ToString()) ? DocumentValidationResponse.Valid : DocumentValidationResponse.Invalid;
    }

    #endregion Validadores
}
