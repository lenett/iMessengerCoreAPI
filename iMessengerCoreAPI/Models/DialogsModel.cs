namespace iMessengerCoreAPI.Models
{
    public struct DialogsModel
    {
        /// <summary>Идентификатор диалога, в который входят идентификаторы клиентов</summary>
        public Guid Data { get; set; }
        public DialogsModel(Guid dialogsGuid)
        {
            Data = dialogsGuid;
        }
    }
}
