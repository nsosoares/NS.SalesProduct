namespace NS.SalesProduct.Helpers.Messages
{
    public static class TriggerMessage
    {
        public static string TriggerMessageMinLength(string field, int min)
            => $"O campo {field} deve ter no mínimo {min} caracteres";

        public static string TriggerMessageMaxLength(string field, int max)
            => $"O campo {field} deve ter no maximo {max} caracteres";

        public static string TriggerMessageRequired(string field)
            => $"O campo {field} deve ser preenchido";

        public static string TriggerMessageGreaterThan(string field, decimal valueToCompare)
           => $"O valor do campo {field} deve ser menor que {valueToCompare}";

        public static string TriggerMessageLessThan(string field, decimal valueToCompare)
         => $"O valor do campo {field} deve ser maior que {valueToCompare}";

        public static string TriggerMessageLessThanWithAnotherProperty(string field, string fieldAnother)
           => $"O valor do campo {field} deve ser menor que o campo {fieldAnother}";

        public static string TriggerMessageListCount(string field)
            => $"Os {field} devem ter pelo menos um selecionado";

        public static string TriggerMessageNotExisting(string id)
           => $"Entidade não encontrada com esse ID {id} em nossa base de dados";

        public static string TriggerMessageExistingItem(string itemName, string itemValue)
         => $"O(a) {itemName} com o valor {itemValue} já existe em nossa base de dados";

        public static string TriggerMessageIsLockedOut()
            => $"Usuario temporariamete bloqueado devido a tentativas de login.";

        public static string TriggerMessageloginFailure()
            => $"Usuario ou senha incorreto.";
    }
}
