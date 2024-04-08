namespace InternetServiceBack.Helpers
{
    public static class MessageHelper
    {
        public static string ErrorCreateInvoice = "Error, no se puedo facturar el pedido";
        public static string ErrorCreateInvoiceProductNotFound = "Error, producto no existe o no fue encontrado";
        public static string ErrorCreateInvoiceProductExecendStock = "Error, uno o mas productos excede el stock existente";

        public static string LoginErrorUserName = "Usuario incorrecto";
        public static string LoginErrorPassword = "Contraseña incorrecta";
        public static string LoginErrorNotActived = "Usuario deshabilitado";

        public static string TokenSesionErrorNotParams = "No se encontro un token sesión";
        public static string TokenSesionErrorValidate = "La sesión no es válida";
        public static string TokenSesionErrorExpired = "La sesión ha caducado";

        public static string RegisterUserErrorParamUserName = "Error, el usuario debe tener entre 4 y 16 caracteres";
        public static string RegisterUserErrorParamPassword = "Error, la contraseña debe tener entre 8 y 16 caracteres";
        public static string RegisterUserErrorExisteUser = "Error, el usuario ya existe en el sistema";
        public static string RegisterUserErrorEx = "Ocurrio un error al ingresar el usuario";

        public static string ChangePasswordErrorId = "Ocurrio un error al buscar el usuario";
        public static string ChangePasswordErrorPassword = "La contraseña anterior no coincide";
        public static string ChangePasswordErrorEx = "Ocurrio un error al realizar el cambio de contraseña";

        public static string GetInvoiceErrorHead = "Ocurrio un error al consultar las facturas";
        public static string GetInvoiceErrorDetail = "Ocurrio un error al consultar el detalle de la factura";
        public static string GetInvoiceErrorDetailNotUser = "No tienes acceso a ver el detalle de esta factura u ocurrio un error";

        public static string TokenSessionErrorExpired = "        public static string TokenSessionErrorExpired = \"No tienes acceso a ver el detalle de esta factura u ocurrio un error\";\n";
        public static string TokenSessionErrorValidate = "No tienes acceso a ver el detalle de esta factura u ocurrio un error";

        public static string RegisterTurnErrorExisteTurn = "Error, el Turno ya existe en el sistema";
        public static string RegisterTurnErrorEx = "Ocurrio un error al ingresar el usuario";

        public static string RegisterAttentionStatusErrorExisteAttentionStatus = "Error, el Status ya existe en el sistema";
        public static string RegisterAttentionStatusErrorEx = "Ocurrio un error al ingresar el status";

        public static string RegisterAttentionTypeErrorExisteAttentionType= "Error, el Status ya existe en el sistema";
        public static string RegisterAttentionTypeErrorEx = "Ocurrio un error al ingresar el status";

        public static string RegisterClientErrorExisteClient = "Error, el Turno ya existe en el sistema";
        public static string RegisterClientErrorEx = "Ocurrio un error al ingresar el usuario";
        
        public static string RegisterClientErrorIdentification= "Error, identificación inválida debe tener entre 10 y 13 dígitos";
        public static string RegisterClientErrorAddress = "Error, dirección inválida debe tener entre 20 y 100 caracteres";

        public static string RegisterMenuErrorExisteMenu = "Error, el Menu  ya existe en el sistema";
        public static string RegisterMenuErrorEx = "Ocurrio un error al ingresar el opcion de menu ";
        
        public static string RegisterCashErrorExisteCash = "Error, el Menu  ya existe en el sistema";
        public static string RegisterCashErrorEx = "Ocurrio un error al ingresar el opcion de menu ";

    }
}
