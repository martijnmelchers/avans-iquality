export class DialogflowResult{
    queryText: string;
    parameters: {
      fields: any
    }
    fulfillmentText: string;
    outputContexts: Array<any>;
    intent: {
      name: string,
      displayName: string
    }
}
