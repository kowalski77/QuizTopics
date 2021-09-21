using System.ComponentModel;

namespace QuizDesigner.Common.ResultModels
{
    public enum ResultCode
    {
        [Description("record.ok")]
        Ok = 200,
        [Description("record.no.content")]
        NoContent = 204,
        [Description("record.not.found")]
        NotFound = 404,
        [Description("record.bad.request")]
        BadRequest = 400,
        [Description("record.internal.server.error")]
        InternalServerError = 501
    }
}