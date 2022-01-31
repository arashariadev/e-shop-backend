using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EShop.Domain.ThirdParty;

public class DeliveryStatusResponse
{
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public List<Datum> Data { get; set; }

        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("info")]
        public List<object> Info { get; set; }

        [JsonProperty("messageCodes")]
        public List<object> MessageCodes { get; set; }

        [JsonProperty("errorCodes")]
        public List<object> ErrorCodes { get; set; }

        [JsonProperty("warningCodes")]
        public List<object> WarningCodes { get; set; }

        [JsonProperty("infoCodes")]
        public List<object> InfoCodes { get; set; }
}

public class Datum 
{
        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("Redelivery")]
        public long Redelivery { get; set; }

        [JsonProperty("RedeliverySum")]
        public long RedeliverySum { get; set; }

        [JsonProperty("RedeliveryNum")]
        public string RedeliveryNum { get; set; }

        [JsonProperty("RedeliveryPayer")]
        public string RedeliveryPayer { get; set; }

        [JsonProperty("OwnerDocumentType")]
        public string OwnerDocumentType { get; set; }

        [JsonProperty("LastCreatedOnTheBasisDocumentType")]
        public string LastCreatedOnTheBasisDocumentType { get; set; }

        [JsonProperty("LastCreatedOnTheBasisPayerType")]
        public string LastCreatedOnTheBasisPayerType { get; set; }

        [JsonProperty("LastCreatedOnTheBasisDateTime")]
        public string LastCreatedOnTheBasisDateTime { get; set; }

        [JsonProperty("LastTransactionStatusGM")]
        public string LastTransactionStatusGm { get; set; }

        [JsonProperty("LastTransactionDateTimeGM")]
        public string LastTransactionDateTimeGm { get; set; }

        [JsonProperty("DateCreated")]
        public string DateCreated { get; set; }

        [JsonProperty("DocumentWeight")]
        public double DocumentWeight { get; set; }

        [JsonProperty("CheckWeight")]
        public long CheckWeight { get; set; }

        [JsonProperty("DocumentCost")]
        public long DocumentCost { get; set; }

        [JsonProperty("SumBeforeCheckWeight")]
        public long SumBeforeCheckWeight { get; set; }

        [JsonProperty("PayerType")]
        public string PayerType { get; set; }

        [JsonProperty("RecipientFullName")]
        public string RecipientFullName { get; set; }

        [JsonProperty("RecipientDateTime")]
        public string RecipientDateTime { get; set; }

        [JsonProperty("ScheduledDeliveryDate")]
        public string ScheduledDeliveryDate { get; set; }

        [JsonProperty("PaymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonProperty("CargoDescriptionString")]
        public string CargoDescriptionString { get; set; }

        [JsonProperty("CargoType")]
        public string CargoType { get; set; }

        [JsonProperty("CitySender")]
        public string CitySender { get; set; }

        [JsonProperty("CityRecipient")]
        public string CityRecipient { get; set; }

        [JsonProperty("WarehouseRecipient")]
        public string WarehouseRecipient { get; set; }

        [JsonProperty("CounterpartyType")]
        public string CounterpartyType { get; set; }

        [JsonProperty("AfterpaymentOnGoodsCost")]
        public long AfterpaymentOnGoodsCost { get; set; }

        [JsonProperty("ServiceType")]
        public string ServiceType { get; set; }

        [JsonProperty("UndeliveryReasonsSubtypeDescription")]
        public string UndeliveryReasonsSubtypeDescription { get; set; }

        [JsonProperty("WarehouseRecipientNumber")]
        public long WarehouseRecipientNumber { get; set; }

        [JsonProperty("LastCreatedOnTheBasisNumber")]
        public string LastCreatedOnTheBasisNumber { get; set; }

        [JsonProperty("PhoneRecipient")]
        public string PhoneRecipient { get; set; }

        [JsonProperty("RecipientFullNameEW")]
        public string RecipientFullNameEw { get; set; }

        [JsonProperty("WarehouseRecipientInternetAddressRef")]
        public Guid WarehouseRecipientInternetAddressRef { get; set; }

        [JsonProperty("MarketplacePartnerToken")]
        public string MarketplacePartnerToken { get; set; }

        [JsonProperty("ClientBarcode")]
        public string ClientBarcode { get; set; }

        [JsonProperty("RecipientAddress")]
        public string RecipientAddress { get; set; }

        [JsonProperty("CounterpartyRecipientDescription")]
        public string CounterpartyRecipientDescription { get; set; }

        [JsonProperty("CounterpartySenderType")]
        public string CounterpartySenderType { get; set; }

        [JsonProperty("DateScan")]
        public DateTimeOffset DateScan { get; set; }

        [JsonProperty("PaymentStatus")]
        public string PaymentStatus { get; set; }

        [JsonProperty("PaymentStatusDate")]
        public string PaymentStatusDate { get; set; }

        [JsonProperty("AmountToPay")]
        public string AmountToPay { get; set; }

        [JsonProperty("AmountPaid")]
        public string AmountPaid { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("StatusCode")]
        public long StatusCode { get; set; }

        [JsonProperty("RefEW")]
        public Guid RefEw { get; set; }

        [JsonProperty("BackwardDeliverySubTypesServices")]
        public List<object> BackwardDeliverySubTypesServices { get; set; }

        [JsonProperty("BackwardDeliverySubTypesActions")]
        public List<object> BackwardDeliverySubTypesActions { get; set; }

        [JsonProperty("UndeliveryReasons")]
        public string UndeliveryReasons { get; set; }
}
    