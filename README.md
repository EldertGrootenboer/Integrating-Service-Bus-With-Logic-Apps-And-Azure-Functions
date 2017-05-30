# Integrating Service Bus With Logic Apps And Azure Functions

Service Bus is a great way to send and receive messages in a decoupled way, and can be used from many languages. When sending a message from the C# SDK, and reading from Service Bus queues from Logic Apps or Azure Functions, you might notice the incoming message has a namespace appended in front of it, as well as some erroneous characters in front and after it. This has to do with how the messages is being serialized when creating a BrokeredMessage using an object. 

Another great service offered by Service Bus is Event Hubs, which is amazing for processing high throughput, high amount of messages. If you're working with Event Hubs, chances are you will also be using Stream Analytics to process the messages. In Stream Analytics you can have various outputs, like PowerBI, DocumentDB, Azure Storage, and also Service Bus Queues. When writing from Stream Analytics to a Service Bus Queue, you will again find that namespace and erroneous characters have been added. 

For more information see my Technet Wiki article [Integrating Service Bus Stack With Logic Apps And Azure Functions](https://social.technet.microsoft.com/wiki/contents/articles/34750.integrating-service-bus-stack-with-logic-apps-and-azure-functions.aspx).
