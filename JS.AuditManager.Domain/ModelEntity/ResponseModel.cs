using JS.AuditManager.Domain.Enum;
using JS.AuditManager.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.AuditManager.Domain.ModelEntity
{
    #region Class Response
    public class ResponseModel : IResponse
    {
       
        /// <summary>
        /// Mensaje general de la respuesta (nunca nulo).
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Indica si ocurrió un error.
        /// </summary>
        public bool DidError { get; set; }

        /// <summary>
        /// Mensaje de error detallado (puede ser nulo si no hubo error).
        /// </summary>
        public string? ErrorMessage { get; set; }

    }


    public class SingleResponse<TModel> : ISingleResponse<TModel>
{
   
    /// <summary>
    /// Mensaje general de la respuesta
    /// </summary>
    public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Indica si ocurrió un error.
        /// </summary>
        public bool DidError { get; set; }

    /// <summary>
    /// Mensaje de error detallado (puede ser nulo si no hubo error).
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Modelo devuelto en la respuesta.
    /// </summary>
    public TModel? Model { get; set; }
}


    public class ListResponse<TModel> : IListResponse<TModel>
    {

        /// <summary>
        /// Mensaje general de la respuesta
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Indica si ocurrió un error.
        /// </summary>
        public bool DidError { get; set; }

        /// <summary>
        /// Mensaje de error detallado (puede ser nulo si no hubo error).
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Colección de modelos devueltos en la respuesta
        /// </summary>
        public IEnumerable<TModel> Model { get; set; }
    }

    #endregion
}
