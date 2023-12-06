using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure;
using System.IO;
using System.IO.Pipes;
using System.Text;

string endpoint = "https://hieuvt19-document-processing.cognitiveservices.azure.com/";
string key = "0069412b7cfa40488580d5593095942f";
AzureKeyCredential credential = new AzureKeyCredential(key);
DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);
Uri invoiceUri = new Uri("https://raw.githubusercontent.com/Azure-Samples/cognitive-services-REST-api-samples/master/curl/form-recognizer/sample-invoice.pdf");
using (FileStream fileStream = new FileStream("D:\\HieuVT19\\Personal\\Me\\Me\\CCCD-After.jpg", FileMode.Open, FileAccess.Read))
{
    AnalyzeDocumentOperation operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-idDocument", fileStream);
    AnalyzeResult result = operation.Value;

    for (int i = 0; i < result.Documents.Count; i++)
    {
        Console.WriteLine($"Document {i}:");

        AnalyzedDocument document = result.Documents[i];

        Console.OutputEncoding = Encoding.UTF8;

        if (document.Fields.TryGetValue("FirstName", out DocumentField firstNameField))
        {
            if (firstNameField.FieldType == DocumentFieldType.String)
            {
                string firstName = firstNameField.Value.AsString();
                Console.WriteLine($"First Name: '{firstName}', with confidence {firstNameField.Confidence}");
            }
        }

        if (document.Fields.TryGetValue("LastName", out DocumentField lastNameField))
        {
            if (lastNameField.FieldType == DocumentFieldType.String)
            {
                string lastName = lastNameField.Value.AsString();
                Console.WriteLine($"Last Name: '{lastName}', with confidence {lastNameField.Confidence}");
            }
        }
        if (document.Fields.TryGetValue("DocumentNumber", out DocumentField documentNumberField))
        {
            if (documentNumberField.FieldType == DocumentFieldType.String)
            {
                string documentNumber = documentNumberField.Value.AsString();
                Console.WriteLine($"Document Number: '{documentNumber}', with confidence {documentNumberField.Confidence}");
            }
        }   
        if (document.Fields.TryGetValue("DateOfExpiration", out DocumentField dateOfExpirationField))
        {
            if (dateOfExpirationField.FieldType == DocumentFieldType.Date)
            {
                string dateOfExpiration = dateOfExpirationField.Value.AsDate().ToString();
                Console.WriteLine($"Date Of Expiration: '{dateOfExpiration}', with confidence {dateOfExpirationField.Confidence}");
            }
        }
    }
}



//for (int i = 0; i < result.Documents.Count; i++)
//{
//    Console.WriteLine($"Document {i}:");

//    AnalyzedDocument document = result.Documents[i];

//    if (document.Fields.TryGetValue("VendorName", out DocumentField vendorNameField))
//    {
//        if (vendorNameField.FieldType == DocumentFieldType.String)
//        {
//            string vendorName = vendorNameField.Value.AsString();
//            Console.WriteLine($"Vendor Name: '{vendorName}', with confidence {vendorNameField.Confidence}");
//        }
//    }

//    if (document.Fields.TryGetValue("CustomerName", out DocumentField customerNameField))
//    {
//        if (customerNameField.FieldType == DocumentFieldType.String)
//        {
//            string customerName = customerNameField.Value.AsString();
//            Console.WriteLine($"Customer Name: '{customerName}', with confidence {customerNameField.Confidence}");
//        }
//    }

//    if (document.Fields.TryGetValue("Items", out DocumentField itemsField))
//    {
//        if (itemsField.FieldType == DocumentFieldType.List)
//        {
//            foreach (DocumentField itemField in itemsField.Value.AsList())
//            {
//                Console.WriteLine("Item:");

//                if (itemField.FieldType == DocumentFieldType.Dictionary)
//                {
//                    IReadOnlyDictionary<string, DocumentField> itemFields = itemField.Value.AsDictionary();

//                    if (itemFields.TryGetValue("Description", out DocumentField itemDescriptionField))
//                    {
//                        if (itemDescriptionField.FieldType == DocumentFieldType.String)
//                        {
//                            string itemDescription = itemDescriptionField.Value.AsString();

//                            Console.WriteLine($"  Description: '{itemDescription}', with confidence {itemDescriptionField.Confidence}");
//                        }
//                    }

//                    if (itemFields.TryGetValue("Amount", out DocumentField itemAmountField))
//                    {
//                        if (itemAmountField.FieldType == DocumentFieldType.Currency)
//                        {
//                            CurrencyValue itemAmount = itemAmountField.Value.AsCurrency();

//                            Console.WriteLine($"  Amount: '{itemAmount.Symbol}{itemAmount.Amount}', with confidence {itemAmountField.Confidence}");
//                        }
//                    }
//                }
//            }
//        }
//    }

//    if (document.Fields.TryGetValue("SubTotal", out DocumentField subTotalField))
//    {
//        if (subTotalField.FieldType == DocumentFieldType.Currency)
//        {
//            CurrencyValue subTotal = subTotalField.Value.AsCurrency();
//            Console.WriteLine($"Sub Total: '{subTotal.Symbol}{subTotal.Amount}', with confidence {subTotalField.Confidence}");
//        }
//    }

//    if (document.Fields.TryGetValue("TotalTax", out DocumentField totalTaxField))
//    {
//        if (totalTaxField.FieldType == DocumentFieldType.Currency)
//        {
//            CurrencyValue totalTax = totalTaxField.Value.AsCurrency();
//            Console.WriteLine($"Total Tax: '{totalTax.Symbol}{totalTax.Amount}', with confidence {totalTaxField.Confidence}");
//        }
//    }

//    if (document.Fields.TryGetValue("InvoiceTotal", out DocumentField invoiceTotalField))
//    {
//        if (invoiceTotalField.FieldType == DocumentFieldType.Currency)
//        {
//            CurrencyValue invoiceTotal = invoiceTotalField.Value.AsCurrency();
//            Console.WriteLine($"Invoice Total: '{invoiceTotal.Symbol}{invoiceTotal.Amount}', with confidence {invoiceTotalField.Confidence}");
//        }
//    }
//}
