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
        ArgumentNullException.ThrowIfNull(cpf);

        cpf = cpf.ClearSymbols();
        if (cpf.Length != 11 || !CPFRegex().IsMatch(cpf))
            return DocumentValidationResponse.WrongSize;


        int[] multiplicadoresPrimeiroDigito = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicadoresSegundoDigito = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        return CheckDigit(cpf, multiplicadoresPrimeiroDigito) && CheckDigit(cpf, multiplicadoresSegundoDigito) ? DocumentValidationResponse.Valid : DocumentValidationResponse.Invalid;
    }

    /// <summary>
    /// Valida um CNPJ.
    /// </summary>
    /// <param name="cnpj">O CNPJ a ser validado.</param>
    /// <returns>Enumerador correspondente DocumentValidationResponse</returns>
    public static DocumentValidationResponse CNPJIsValid(string cnpj)
    {
        ArgumentNullException.ThrowIfNull(cnpj);

        cnpj = cnpj.ClearSymbols();
        if (cnpj.Length != 14 || !CNPJRegex().IsMatch(cnpj))
            return DocumentValidationResponse.WrongSize;

        int[] multiplicadoresPrimeiroDigito = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicadoresSegundoDigito = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        return CheckDigit(cnpj, multiplicadoresPrimeiroDigito) && CheckDigit(cnpj, multiplicadoresSegundoDigito) ? DocumentValidationResponse.Valid : DocumentValidationResponse.Invalid;
    }

    /// <summary>
    /// Valida um PIS.
    /// </summary>
    /// <param name="pis">O PIS a ser validado.</param>
    /// <returns>Enumerador correspondente DocumentValidationResponse</returns>
    public static DocumentValidationResponse PISsValid(string pis)
    {
        ArgumentNullException.ThrowIfNull(pis);

        pis = pis.ClearSymbols();
        if (pis.Length != 11 || !PISRegex().IsMatch(pis))
            return DocumentValidationResponse.WrongSize;

        int[] multiplicadores = [3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        return CheckDigit(pis, multiplicadores) ? DocumentValidationResponse.Valid : DocumentValidationResponse.Invalid;
    }

    #endregion Validadores

    #region Private

    /// <summary>
    /// Realiza a verificação do digito conforme o multiplicador
    /// </summary>
    /// <param name="numero">Numero</param>
    /// <param name="multiplicadores">multiplicadores</param>
    /// <returns>True se for válido, False caso contrário</returns>
    private static bool CheckDigit(string numero, int[] multiplicadores)
    {
        int soma = 0;
        for (int i = 0; i < multiplicadores.Length; i++)
            soma += int.Parse(numero[i].ToString()) * multiplicadores[i];

        int resto = soma % 11;
        int digitoVerificador = resto < 2 ? 0 : 11 - resto;

        return digitoVerificador == int.Parse(numero[^1].ToString());
    }

    #endregion Private

}
