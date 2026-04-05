namespace rec ControlApiV1.Types

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type RequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type RuleType =
    | [<CompiledName "amqp/external">] AmqpExternal
    member this.Format() =
        match this with
        | AmqpExternal -> "amqp/external"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Status =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type Headers =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of Headers with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Headers = { name = None; value = None }

type Target =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<Headers>>
      ///Reject delivery of the message if the route does not exist, otherwise fail silently.
      mandatoryRoute: Option<bool>
      ///You can optionally override the default TTL on a queue and specify a TTL in minutes for messages to be persisted. It is unusual to change the default TTL, so if this field is left empty, the default TTL for the queue will be used.
      messageTtl: Option<int>
      ///Marks the message as persistent, instructing the broker to write it to disk if it is in a durable queue.
      persistentMessages: Option<bool>
      ///The AMQP routing key. See this &amp;lt;a href="https://knowledge.ably.com/what-is-the-format-of-the-routingkey-for-an-amqp-or-kinesis-reactor-rule"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      routingKey: Option<string>
      url: Option<string> }
    ///Creates an instance of Target with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Target =
        { enveloped = None
          format = None
          headers = None
          mandatoryRoute = None
          messageTtl = None
          persistentMessages = None
          routingKey = None
          url = None }

type amqpexternalrulepatch =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: Option<RequestMode>
      ///The type of rule. In this case AMQP external (using Firehose). See the &amp;lt;a href="https://ably.com/documentation/general/firehose"&amp;gt;Ably documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: Option<RuleType>
      source: Option<rulesource>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<Status>
      target: Option<Target> }
    ///Creates an instance of amqpexternalrulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): amqpexternalrulepatch =
        { requestMode = None
          ruleType = None
          source = None
          status = None
          target = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqpexternalrulepostRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqpexternalrulepostRuleType =
    | [<CompiledName "amqp/external">] AmqpExternal
    member this.Format() =
        match this with
        | AmqpExternal -> "amqp/external"

type amqpexternalrulepostTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of amqpexternalrulepostTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): amqpexternalrulepostTargetHeaders = { name = None; value = None }

type amqpexternalrulepostTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<amqpexternalrulepostTargetHeaders>>
      ///Reject delivery of the message if the route does not exist, otherwise fail silently.
      mandatoryRoute: bool
      ///You can optionally override the default TTL on a queue and specify a TTL in minutes for messages to be persisted. It is unusual to change the default TTL, so if this field is left empty, the default TTL for the queue will be used.
      messageTtl: Option<int>
      ///Marks the message as persistent, instructing the broker to write it to disk if it is in a durable queue.
      persistentMessages: bool
      ///The AMQP routing key. See this &amp;lt;a href="https://knowledge.ably.com/what-is-the-format-of-the-routingkey-for-an-amqp-or-kinesis-reactor-rule"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      routingKey: string
      url: string }
    ///Creates an instance of amqpexternalrulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (mandatoryRoute: bool, persistentMessages: bool, routingKey: string, url: string): amqpexternalrulepostTarget =
        { enveloped = None
          format = None
          headers = None
          mandatoryRoute = mandatoryRoute
          messageTtl = None
          persistentMessages = persistentMessages
          routingKey = routingKey
          url = url }

type amqpexternalrulepost =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: amqpexternalrulepostRequestMode
      ///The type of rule. In this case AMQP external (using Firehose). See the &amp;lt;a href="https://ably.com/documentation/general/firehose"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: amqpexternalrulepostRuleType
      source: rulesource
      target: amqpexternalrulepostTarget }
    ///Creates an instance of amqpexternalrulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: amqpexternalrulepostRequestMode,
                          ruleType: amqpexternalrulepostRuleType,
                          source: rulesource,
                          target: amqpexternalrulepostTarget): amqpexternalrulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqpexternalruleresponseRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqpexternalruleresponseRuleType =
    | [<CompiledName "amqp/external">] AmqpExternal
    member this.Format() =
        match this with
        | AmqpExternal -> "amqp/external"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqpexternalruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type amqpexternalruleresponseTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of amqpexternalruleresponseTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): amqpexternalruleresponseTargetHeaders = { name = None; value = None }

type amqpexternalruleresponseTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<amqpexternalruleresponseTargetHeaders>>
      ///Reject delivery of the message if the route does not exist, otherwise fail silently.
      mandatoryRoute: bool
      ///You can optionally override the default TTL on a queue and specify a TTL in minutes for messages to be persisted. It is unusual to change the default TTL, so if this field is left empty, the default TTL for the queue will be used.
      messageTtl: Option<int>
      ///Marks the message as persistent, instructing the broker to write it to disk if it is in a durable queue.
      persistentMessages: bool
      ///The AMQP routing key. See this &amp;lt;a href="https://knowledge.ably.com/what-is-the-format-of-the-routingkey-for-an-amqp-or-kinesis-reactor-rule"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      routingKey: string
      url: string }
    ///Creates an instance of amqpexternalruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (mandatoryRoute: bool, persistentMessages: bool, routingKey: string, url: string): amqpexternalruleresponseTarget =
        { enveloped = None
          format = None
          headers = None
          mandatoryRoute = mandatoryRoute
          messageTtl = None
          persistentMessages = persistentMessages
          routingKey = routingKey
          url = url }

type amqpexternalruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: amqpexternalruleresponseRequestMode
      ///The type of rule. In this case AMQP external (using Firehose). See the &amp;lt;a href="https://ably.com/documentation/general/firehose"&amp;gt;Ably documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: amqpexternalruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<amqpexternalruleresponseStatus>
      target: amqpexternalruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of amqpexternalruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: amqpexternalruleresponseRequestMode,
                          ruleType: amqpexternalruleresponseRuleType,
                          source: rulesource,
                          target: amqpexternalruleresponseTarget): amqpexternalruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqprulepatchRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqprulepatchRuleType =
    | [<CompiledName "amqp">] Amqp
    member this.Format() =
        match this with
        | Amqp -> "amqp"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqprulepatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type amqprulepatchTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of amqprulepatchTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): amqprulepatchTargetHeaders = { name = None; value = None }

type amqprulepatchTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<amqprulepatchTargetHeaders>>
      queueId: Option<string> }
    ///Creates an instance of amqprulepatchTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): amqprulepatchTarget =
        { enveloped = None
          format = None
          headers = None
          queueId = None }

type amqprulepatch =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: Option<amqprulepatchRequestMode>
      ///The type of rule. In this case AMQP. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: Option<amqprulepatchRuleType>
      source: Option<rulesource>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<amqprulepatchStatus>
      target: Option<amqprulepatchTarget> }
    ///Creates an instance of amqprulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): amqprulepatch =
        { requestMode = None
          ruleType = None
          source = None
          status = None
          target = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqprulepostRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqprulepostRuleType =
    | [<CompiledName "amqp">] Amqp
    member this.Format() =
        match this with
        | Amqp -> "amqp"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqprulepostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type amqprulepostTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of amqprulepostTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): amqprulepostTargetHeaders = { name = None; value = None }

type amqprulepostTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<amqprulepostTargetHeaders>>
      queueId: string }
    ///Creates an instance of amqprulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (queueId: string): amqprulepostTarget =
        { enveloped = None
          format = None
          headers = None
          queueId = queueId }

type amqprulepost =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: amqprulepostRequestMode
      ///The type of rule. In this case AMQP. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: amqprulepostRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<amqprulepostStatus>
      target: amqprulepostTarget }
    ///Creates an instance of amqprulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: amqprulepostRequestMode,
                          ruleType: amqprulepostRuleType,
                          source: rulesource,
                          target: amqprulepostTarget): amqprulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqpruleresponseRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqpruleresponseRuleType =
    | [<CompiledName "amqp">] Amqp
    member this.Format() =
        match this with
        | Amqp -> "amqp"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type amqpruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type amqpruleresponseTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of amqpruleresponseTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): amqpruleresponseTargetHeaders = { name = None; value = None }

type amqpruleresponseTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<amqpruleresponseTargetHeaders>>
      queueId: string }
    ///Creates an instance of amqpruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (queueId: string): amqpruleresponseTarget =
        { enveloped = None
          format = None
          headers = None
          queueId = queueId }

type amqpruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: amqpruleresponseRequestMode
      ///The type of rule. In this case AMQP. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: amqpruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<amqpruleresponseStatus>
      target: amqpruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of amqpruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: amqpruleresponseRequestMode,
                          ruleType: amqpruleresponseRuleType,
                          source: rulesource,
                          target: amqpruleresponseTarget): amqpruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type apppatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type apppatch =
    { ///The Apple Push Notification service certificate.
      apnsCertificate: Option<string>
      ///The Apple Push Notification service private key.
      apnsPrivateKey: Option<string>
      ///The Apple Push Notification service sandbox endpoint.
      apnsUseSandboxEndpoint: Option<bool>
      ///The Firebase Cloud Messaging key.
      fcmKey: Option<string>
      ///The name of the application for your reference only.
      name: Option<string>
      ///The status of the application. Can be `enabled` or `disabled`. Enabled means available to accept inbound connections and all services are available.
      status: Option<apppatchStatus>
      ///Enforce TLS for all connections.
      tlsOnly: Option<bool> }
    ///Creates an instance of apppatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): apppatch =
        { apnsCertificate = None
          apnsPrivateKey = None
          apnsUseSandboxEndpoint = None
          fcmKey = None
          name = None
          status = None
          tlsOnly = None }

type apppkcs12 =
    { ///The `.p12` file containing the app's APNs information.
      p12File: string
      ///The password for the corresponding `.p12` file.
      p12Pass: string }
    ///Creates an instance of apppkcs12 with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (p12File: string, p12Pass: string): apppkcs12 = { p12File = p12File; p12Pass = p12Pass }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type apppostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type apppost =
    { ///The Apple Push Notification service certificate.
      apnsCertificate: Option<string>
      ///The Apple Push Notification service private key.
      apnsPrivateKey: Option<string>
      ///The Apple Push Notification service sandbox endpoint.
      apnsUseSandboxEndpoint: Option<bool>
      ///The Firebase Cloud Messaging key.
      fcmKey: Option<string>
      ///The name of the application for your reference only.
      name: string
      ///The status of the application. Can be `enabled` or `disabled`. Enabled means available to accept inbound connections and all services are available.
      status: Option<apppostStatus>
      ///Enforce TLS for all connections.
      tlsOnly: Option<bool> }
    ///Creates an instance of apppost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (name: string): apppost =
        { apnsCertificate = None
          apnsPrivateKey = None
          apnsUseSandboxEndpoint = None
          fcmKey = None
          name = name
          status = None
          tlsOnly = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type appresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type appresponse =
    { ///A link self-referencing the app that has been created.
      _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The ID of your Ably account.
      accountId: Option<string>
      ///Apple Push Notification service endpoint.
      apnsUseSandboxEndpoint: Option<bool>
      ///The application ID.
      id: Option<string>
      ///The application name.
      name: Option<string>
      ///The application status. Disabled applications will not accept new connections and will return an error to all clients.
      status: Option<appresponseStatus>
      ///Enforce TLS for all connections. This setting overrides any channel setting.
      tlsOnly: Option<bool> }
    ///Creates an instance of appresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): appresponse =
        { _links = None
          accountId = None
          apnsUseSandboxEndpoint = None
          id = None
          name = None
          status = None
          tlsOnly = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type AuthenticationMode =
    | [<CompiledName "credentials">] Credentials
    member this.Format() =
        match this with
        | Credentials -> "credentials"

type awsaccesskeys =
    { ///The AWS key ID for the AWS IAM user. See this &amp;lt;a href="https://knowledge.ably.com/authentication-for-reactor-rules-for-aws-reactor-events-for-lambda-functions-reactor-firehose-for-aws-sqs-and-kinesis"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      accessKeyId: string
      ///Authentication method is using AWS credentials (AWS key ID and secret key).
      authenticationMode: Option<AuthenticationMode>
      ///The AWS secret key for the AWS IAM user. See this &amp;lt;a href="https://knowledge.ably.com/authentication-for-reactor-rules-for-aws-reactor-events-for-lambda-functions-reactor-firehose-for-aws-sqs-and-kinesis"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      secretAccessKey: string }
    ///Creates an instance of awsaccesskeys with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (accessKeyId: string, secretAccessKey: string): awsaccesskeys =
        { accessKeyId = accessKeyId
          authenticationMode = None
          secretAccessKey = secretAccessKey }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awsaccesskeysresponseAuthenticationMode =
    | [<CompiledName "credentials">] Credentials
    member this.Format() =
        match this with
        | Credentials -> "credentials"

type awsaccesskeysresponse =
    { ///The AWS key ID for the AWS IAM user. See this &amp;lt;a href="https://knowledge.ably.com/authentication-for-reactor-rules-for-aws-reactor-events-for-lambda-functions-reactor-firehose-for-aws-sqs-and-kinesis"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      accessKeyId: Option<string>
      ///Authentication method is using AWS credentials (AWS key ID and secret key).
      authenticationMode: Option<awsaccesskeysresponseAuthenticationMode> }
    ///Creates an instance of awsaccesskeysresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): awsaccesskeysresponse =
        { accessKeyId = None
          authenticationMode = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awsassumeroleAuthenticationMode =
    | [<CompiledName "assumeRole">] AssumeRole
    member this.Format() =
        match this with
        | AssumeRole -> "assumeRole"

type awsassumerole =
    { ///If you are using the "ARN of an assumable role" authentication method, this is your Assume Role ARN. See this &amp;lt;a href="https://knowledge.ably.com/authentication-for-reactor-rules-for-aws-reactor-events-for-lambda-functions-reactor-firehose-for-aws-sqs-and-kinesis"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      assumeRoleArn: string
      ///Authentication method is using the ARN of an assumable role. See this &amp;lt;a href="https://knowledge.ably.com/authentication-for-reactor-rules-for-aws-reactor-events-for-lambda-functions-reactor-firehose-for-aws-sqs-and-kinesis"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      authenticationMode: Option<awsassumeroleAuthenticationMode> }
    ///Creates an instance of awsassumerole with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (assumeRoleArn: string): awsassumerole =
        { assumeRoleArn = assumeRoleArn
          authenticationMode = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisrulepatchRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisrulepatchRuleType =
    | [<CompiledName "aws/kinesis">] AwsKinesis
    member this.Format() =
        match this with
        | AwsKinesis -> "aws/kinesis"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisrulepatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Format =
    | [<CompiledName "json">] Json
    member this.Format() =
        match this with
        | Json -> "json"

type awskinesisrulepatchTarget =
    { authentication: Option<Newtonsoft.Json.Linq.JToken>
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a text-based encoding.
      format: Option<Format>
      ///The AWS Kinesis partition key. See this &amp;lt;a href="https://knowledge.ably.com/what-is-the-format-of-the-routingkey-for-an-amqp-or-kinesis-reactor-rule"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      partitionKey: Option<string>
      ///The region is which AWS Kinesis is hosted. See the &amp;lt;a href="https://docs.aws.amazon.com/general/latest/gr/rande.html#lambda_region"&amp;gt;AWS documentation&amp;lt;/a&amp;gt; for more detail.
      region: Option<string>
      ///The name of your AWS Kinesis Stream.
      streamName: Option<string> }
    ///Creates an instance of awskinesisrulepatchTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): awskinesisrulepatchTarget =
        { authentication = None
          enveloped = None
          format = None
          partitionKey = None
          region = None
          streamName = None }

type awskinesisrulepatch =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: Option<awskinesisrulepatchRequestMode>
      ///The type of rule. In this case AWS Kinesis. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: Option<awskinesisrulepatchRuleType>
      source: Option<rulesource>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<awskinesisrulepatchStatus>
      target: Option<awskinesisrulepatchTarget> }
    ///Creates an instance of awskinesisrulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): awskinesisrulepatch =
        { requestMode = None
          ruleType = None
          source = None
          status = None
          target = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisrulepostRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisrulepostRuleType =
    | [<CompiledName "aws/kinesis">] AwsKinesis
    member this.Format() =
        match this with
        | AwsKinesis -> "aws/kinesis"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisrulepostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisrulepostTargetFormat =
    | [<CompiledName "json">] Json
    member this.Format() =
        match this with
        | Json -> "json"

type awskinesisrulepostTarget =
    { authentication: Newtonsoft.Json.Linq.JToken
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a text-based encoding.
      format: awskinesisrulepostTargetFormat
      ///The AWS Kinesis partition key. See this &amp;lt;a href="https://knowledge.ably.com/what-is-the-format-of-the-routingkey-for-an-amqp-or-kinesis-reactor-rule"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      partitionKey: string
      ///The region is which AWS Kinesis is hosted. See the &amp;lt;a href="https://docs.aws.amazon.com/general/latest/gr/rande.html#lambda_region"&amp;gt;AWS documentation&amp;lt;/a&amp;gt; for more detail.
      region: string
      ///The name of your AWS Kinesis Stream.
      streamName: string }
    ///Creates an instance of awskinesisrulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (authentication: Newtonsoft.Json.Linq.JToken,
                          format: awskinesisrulepostTargetFormat,
                          partitionKey: string,
                          region: string,
                          streamName: string): awskinesisrulepostTarget =
        { authentication = authentication
          enveloped = None
          format = format
          partitionKey = partitionKey
          region = region
          streamName = streamName }

type awskinesisrulepost =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: awskinesisrulepostRequestMode
      ///The type of rule. In this case AWS Kinesis. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: awskinesisrulepostRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<awskinesisrulepostStatus>
      target: awskinesisrulepostTarget }
    ///Creates an instance of awskinesisrulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: awskinesisrulepostRequestMode,
                          ruleType: awskinesisrulepostRuleType,
                          source: rulesource,
                          target: awskinesisrulepostTarget): awskinesisrulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisruleresponseRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisruleresponseRuleType =
    | [<CompiledName "aws/kinesis">] AwsKinesis
    member this.Format() =
        match this with
        | AwsKinesis -> "aws/kinesis"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awskinesisruleresponseTargetFormat =
    | [<CompiledName "json">] Json
    member this.Format() =
        match this with
        | Json -> "json"

type awskinesisruleresponseTarget =
    { authentication: Newtonsoft.Json.Linq.JToken
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a text-based encoding.
      format: awskinesisruleresponseTargetFormat
      ///The AWS Kinesis partition key. See this &amp;lt;a href="https://knowledge.ably.com/what-is-the-format-of-the-routingkey-for-an-amqp-or-kinesis-reactor-rule"&amp;gt;Ably knowledge base article&amp;lt;/a&amp;gt; for details.
      partitionKey: string
      ///The region is which AWS Kinesis is hosted. See the &amp;lt;a href="https://docs.aws.amazon.com/general/latest/gr/rande.html#lambda_region"&amp;gt;AWS documentation&amp;lt;/a&amp;gt; for more detail.
      region: string
      ///The name of your AWS Kinesis Stream.
      streamName: string }
    ///Creates an instance of awskinesisruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (authentication: Newtonsoft.Json.Linq.JToken,
                          format: awskinesisruleresponseTargetFormat,
                          partitionKey: string,
                          region: string,
                          streamName: string): awskinesisruleresponseTarget =
        { authentication = authentication
          enveloped = None
          format = format
          partitionKey = partitionKey
          region = region
          streamName = streamName }

type awskinesisruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: awskinesisruleresponseRequestMode
      ///The type of rule. In this case AWS Kinesis. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: awskinesisruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<awskinesisruleresponseStatus>
      target: awskinesisruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of awskinesisruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: awskinesisruleresponseRequestMode,
                          ruleType: awskinesisruleresponseRuleType,
                          source: rulesource,
                          target: awskinesisruleresponseTarget): awskinesisruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awslambdarulepatchRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awslambdarulepatchRuleType =
    | [<CompiledName "aws/lambda">] AwsLambda
    member this.Format() =
        match this with
        | AwsLambda -> "aws/lambda"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awslambdarulepatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type awslambdarulepatchTarget =
    { authentication: Newtonsoft.Json.Linq.JToken
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///The name of your AWS Lambda Function.
      functionName: string
      ///The region is which your AWS Lambda Function is hosted. See the &amp;lt;a href="https://docs.aws.amazon.com/general/latest/gr/rande.html#lambda_region"&amp;gt;AWS documentation&amp;lt;/a&amp;gt; for more detail.
      region: string }
    ///Creates an instance of awslambdarulepatchTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (authentication: Newtonsoft.Json.Linq.JToken, functionName: string, region: string): awslambdarulepatchTarget =
        { authentication = authentication
          enveloped = None
          functionName = functionName
          region = region }

type awslambdarulepatch =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: awslambdarulepatchRequestMode
      ///The type of rule. In this case AWS Lambda. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;Ably documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: awslambdarulepatchRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<awslambdarulepatchStatus>
      target: awslambdarulepatchTarget }
    ///Creates an instance of awslambdarulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: awslambdarulepatchRequestMode,
                          ruleType: awslambdarulepatchRuleType,
                          source: rulesource,
                          target: awslambdarulepatchTarget): awslambdarulepatch =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awslambdarulepostRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awslambdarulepostRuleType =
    | [<CompiledName "aws/lambda">] AwsLambda
    member this.Format() =
        match this with
        | AwsLambda -> "aws/lambda"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awslambdarulepostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type awslambdarulepostTarget =
    { authentication: Newtonsoft.Json.Linq.JToken
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///The name of your AWS Lambda Function.
      functionName: string
      ///The region is which your AWS Lambda Function is hosted. See the &amp;lt;a href="https://docs.aws.amazon.com/general/latest/gr/rande.html#lambda_region"&amp;gt;AWS documentation&amp;lt;/a&amp;gt; for more detail.
      region: string }
    ///Creates an instance of awslambdarulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (authentication: Newtonsoft.Json.Linq.JToken, functionName: string, region: string): awslambdarulepostTarget =
        { authentication = authentication
          enveloped = None
          functionName = functionName
          region = region }

type awslambdarulepost =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: awslambdarulepostRequestMode
      ///The type of rule. In this case AWS Lambda. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: awslambdarulepostRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<awslambdarulepostStatus>
      target: awslambdarulepostTarget }
    ///Creates an instance of awslambdarulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: awslambdarulepostRequestMode,
                          ruleType: awslambdarulepostRuleType,
                          source: rulesource,
                          target: awslambdarulepostTarget): awslambdarulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awslambdaruleresponseRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awslambdaruleresponseRuleType =
    | [<CompiledName "aws/lambda">] AwsLambda
    member this.Format() =
        match this with
        | AwsLambda -> "aws/lambda"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awslambdaruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type awslambdaruleresponseTarget =
    { authentication: Newtonsoft.Json.Linq.JToken
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///The name of your AWS Lambda Function.
      functionName: string
      ///The region is which your AWS Lambda Function is hosted. See the &amp;lt;a href="https://docs.aws.amazon.com/general/latest/gr/rande.html#lambda_region"&amp;gt;AWS documentation&amp;lt;/a&amp;gt; for more detail.
      region: string }
    ///Creates an instance of awslambdaruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (authentication: Newtonsoft.Json.Linq.JToken, functionName: string, region: string): awslambdaruleresponseTarget =
        { authentication = authentication
          enveloped = None
          format = None
          functionName = functionName
          region = region }

type awslambdaruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: awslambdaruleresponseRequestMode
      ///The type of rule. In this case AWS Lambda. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: awslambdaruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<awslambdaruleresponseStatus>
      target: awslambdaruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of awslambdaruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: awslambdaruleresponseRequestMode,
                          ruleType: awslambdaruleresponseRuleType,
                          source: rulesource,
                          target: awslambdaruleresponseTarget): awslambdaruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awssqsrulepatchRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awssqsrulepatchRuleType =
    | [<CompiledName "aws/sqs">] AwsSqs
    member this.Format() =
        match this with
        | AwsSqs -> "aws/sqs"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awssqsrulepatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type awssqsrulepatchTarget =
    { authentication: Option<Newtonsoft.Json.Linq.JToken>
      ///Your AWS account ID.
      awsAccountId: Option<string>
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///The AWS SQS queue name.
      queueName: Option<string>
      ///The region is which AWS SQS is hosted. See the &amp;lt;a href="https://docs.aws.amazon.com/general/latest/gr/rande.html#lambda_region"&amp;gt;AWS documentation&amp;lt;/a&amp;gt; for more detail.
      region: Option<string> }
    ///Creates an instance of awssqsrulepatchTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): awssqsrulepatchTarget =
        { authentication = None
          awsAccountId = None
          enveloped = None
          format = None
          queueName = None
          region = None }

type awssqsrulepatch =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: Option<awssqsrulepatchRequestMode>
      ///The type of rule. In this case AWS SQS. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: Option<awssqsrulepatchRuleType>
      source: Option<rulesource>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<awssqsrulepatchStatus>
      target: Option<awssqsrulepatchTarget> }
    ///Creates an instance of awssqsrulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): awssqsrulepatch =
        { requestMode = None
          ruleType = None
          source = None
          status = None
          target = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awssqsrulepostRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awssqsrulepostRuleType =
    | [<CompiledName "aws/sqs">] AwsSqs
    member this.Format() =
        match this with
        | AwsSqs -> "aws/sqs"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awssqsrulepostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type awssqsrulepostTarget =
    { authentication: Newtonsoft.Json.Linq.JToken
      ///Your AWS account ID.
      awsAccountId: string
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///The AWS SQS queue name.
      queueName: string
      ///The region is which AWS SQS is hosted. See the &amp;lt;a href="https://docs.aws.amazon.com/general/latest/gr/rande.html#lambda_region"&amp;gt;AWS documentation&amp;lt;/a&amp;gt; for more detail.
      region: string }
    ///Creates an instance of awssqsrulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (authentication: Newtonsoft.Json.Linq.JToken,
                          awsAccountId: string,
                          queueName: string,
                          region: string): awssqsrulepostTarget =
        { authentication = authentication
          awsAccountId = awsAccountId
          enveloped = None
          format = None
          queueName = queueName
          region = region }

type awssqsrulepost =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: awssqsrulepostRequestMode
      ///The type of rule. In this case AWS SQS. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: awssqsrulepostRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<awssqsrulepostStatus>
      target: awssqsrulepostTarget }
    ///Creates an instance of awssqsrulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: awssqsrulepostRequestMode,
                          ruleType: awssqsrulepostRuleType,
                          source: rulesource,
                          target: awssqsrulepostTarget): awssqsrulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awssqsruleresponseRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awssqsruleresponseRuleType =
    | [<CompiledName "aws/sqs">] AwsSqs
    member this.Format() =
        match this with
        | AwsSqs -> "aws/sqs"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type awssqsruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type awssqsruleresponseTarget =
    { authentication: Newtonsoft.Json.Linq.JToken
      ///Your AWS account ID.
      awsAccountId: string
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      format: Option<string>
      ///The AWS SQS queue name.
      queueName: string
      ///The region is which AWS SQS is hosted. See the &amp;lt;a href="https://docs.aws.amazon.com/general/latest/gr/rande.html#lambda_region"&amp;gt;AWS documentation&amp;lt;/a&amp;gt; for more detail.
      region: string }
    ///Creates an instance of awssqsruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (authentication: Newtonsoft.Json.Linq.JToken,
                          awsAccountId: string,
                          queueName: string,
                          region: string): awssqsruleresponseTarget =
        { authentication = authentication
          awsAccountId = awsAccountId
          enveloped = None
          format = None
          queueName = queueName
          region = region }

type awssqsruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: awssqsruleresponseRequestMode
      ///The type of rule. In this case AWS SQS. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: awssqsruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<awssqsruleresponseStatus>
      target: awssqsruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of awssqsruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: awssqsruleresponseRequestMode,
                          ruleType: awssqsruleresponseRuleType,
                          source: rulesource,
                          target: awssqsruleresponseTarget): awssqsruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionrulepatchRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionrulepatchRuleType =
    | [<CompiledName "http/azure-function">] HttpAzureFunction
    member this.Format() =
        match this with
        | HttpAzureFunction -> "http/azure-function"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionrulepatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionrulepatchTargetFormat =
    | [<CompiledName "json">] Json
    member this.Format() =
        match this with
        | Json -> "json"

type azurefunctionrulepatchTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of azurefunctionrulepatchTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): azurefunctionrulepatchTargetHeaders = { name = None; value = None }

type azurefunctionrulepatchTarget =
    { ///The Microsoft Azure Application ID. You can find your Microsoft Azure Application ID as shown in this &amp;lt;a href="https://dev.applicationinsights.io/documentation/Authorization/API-key-and-App-ID"&amp;gt;article&amp;lt;/a&amp;gt;.
      azureAppId: Option<string>
      ///The name of your Microsoft Azure Function.
      azureFunctionName: Option<string>
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a text-based encoding.
      format: Option<azurefunctionrulepatchTargetFormat>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<azurefunctionrulepatchTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string> }
    ///Creates an instance of azurefunctionrulepatchTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): azurefunctionrulepatchTarget =
        { azureAppId = None
          azureFunctionName = None
          enveloped = None
          format = None
          headers = None
          signingKeyId = None }

type azurefunctionrulepatch =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: Option<azurefunctionrulepatchRequestMode>
      ///The type of rule. In this case Microsoft Azure Function. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: Option<azurefunctionrulepatchRuleType>
      source: Option<rulesource>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<azurefunctionrulepatchStatus>
      target: Option<azurefunctionrulepatchTarget> }
    ///Creates an instance of azurefunctionrulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): azurefunctionrulepatch =
        { requestMode = None
          ruleType = None
          source = None
          status = None
          target = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionrulepostRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionrulepostRuleType =
    | [<CompiledName "http/azure-function">] HttpAzureFunction
    member this.Format() =
        match this with
        | HttpAzureFunction -> "http/azure-function"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionrulepostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionrulepostTargetFormat =
    | [<CompiledName "json">] Json
    member this.Format() =
        match this with
        | Json -> "json"

type azurefunctionrulepostTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of azurefunctionrulepostTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): azurefunctionrulepostTargetHeaders = { name = None; value = None }

type azurefunctionrulepostTarget =
    { ///The Microsoft Azure Application ID. You can find your Microsoft Azure Application ID as shown in this &amp;lt;a href="https://dev.applicationinsights.io/documentation/Authorization/API-key-and-App-ID"&amp;gt;article&amp;lt;/a&amp;gt;.
      azureAppId: string
      ///The name of your Microsoft Azure Function.
      azureFunctionName: string
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a text-based encoding.
      format: Option<azurefunctionrulepostTargetFormat>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<azurefunctionrulepostTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string> }
    ///Creates an instance of azurefunctionrulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (azureAppId: string, azureFunctionName: string): azurefunctionrulepostTarget =
        { azureAppId = azureAppId
          azureFunctionName = azureFunctionName
          enveloped = None
          format = None
          headers = None
          signingKeyId = None }

type azurefunctionrulepost =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: azurefunctionrulepostRequestMode
      ///The type of rule. In this case Microsoft Azure Function. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: azurefunctionrulepostRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<azurefunctionrulepostStatus>
      target: azurefunctionrulepostTarget }
    ///Creates an instance of azurefunctionrulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: azurefunctionrulepostRequestMode,
                          ruleType: azurefunctionrulepostRuleType,
                          source: rulesource,
                          target: azurefunctionrulepostTarget): azurefunctionrulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionruleresponseRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionruleresponseRuleType =
    | [<CompiledName "http/azure-function">] HttpAzureFunction
    member this.Format() =
        match this with
        | HttpAzureFunction -> "http/azure-function"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type azurefunctionruleresponseTargetFormat =
    | [<CompiledName "json">] Json
    member this.Format() =
        match this with
        | Json -> "json"

type azurefunctionruleresponseTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of azurefunctionruleresponseTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): azurefunctionruleresponseTargetHeaders = { name = None; value = None }

type azurefunctionruleresponseTarget =
    { ///The Microsoft Azure Application ID. You can find your Microsoft Azure Application ID as shown in this &amp;lt;a href="https://dev.applicationinsights.io/documentation/Authorization/API-key-and-App-ID"&amp;gt;article&amp;lt;/a&amp;gt;.
      azureAppId: string
      ///The name of your Microsoft Azure Function.
      azureFunctionName: string
      ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a text-based encoding.
      format: Option<azurefunctionruleresponseTargetFormat>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<azurefunctionruleresponseTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string> }
    ///Creates an instance of azurefunctionruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (azureAppId: string, azureFunctionName: string): azurefunctionruleresponseTarget =
        { azureAppId = azureAppId
          azureFunctionName = azureFunctionName
          enveloped = None
          format = None
          headers = None
          signingKeyId = None }

type azurefunctionruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: azurefunctionruleresponseRequestMode
      ///The type of rule. In this case Microsoft Azure Function. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: azurefunctionruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<azurefunctionruleresponseStatus>
      target: azurefunctionruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of azurefunctionruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: azurefunctionruleresponseRequestMode,
                          ruleType: azurefunctionruleresponseRuleType,
                          source: rulesource,
                          target: azurefunctionruleresponseTarget): azurefunctionruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type cloudflareworkerrulepatchRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type cloudflareworkerrulepatchRuleType =
    | [<CompiledName "http/cloudflare-worker">] HttpCloudflareWorker
    member this.Format() =
        match this with
        | HttpCloudflareWorker -> "http/cloudflare-worker"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type cloudflareworkerrulepatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type cloudflareworkerrulepatchTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of cloudflareworkerrulepatchTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): cloudflareworkerrulepatchTargetHeaders = { name = None; value = None }

type cloudflareworkerrulepatchTarget =
    { ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<cloudflareworkerrulepatchTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string>
      url: Option<string> }
    ///Creates an instance of cloudflareworkerrulepatchTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): cloudflareworkerrulepatchTarget =
        { headers = None
          signingKeyId = None
          url = None }

type cloudflareworkerrulepatch =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: Option<cloudflareworkerrulepatchRequestMode>
      ///The type of rule. In this case Cloudflare Worker. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: Option<cloudflareworkerrulepatchRuleType>
      source: Option<rulesource>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<cloudflareworkerrulepatchStatus>
      target: Option<cloudflareworkerrulepatchTarget> }
    ///Creates an instance of cloudflareworkerrulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): cloudflareworkerrulepatch =
        { requestMode = None
          ruleType = None
          source = None
          status = None
          target = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type cloudflareworkerrulepostRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type cloudflareworkerrulepostRuleType =
    | [<CompiledName "http/cloudflare-worker">] HttpCloudflareWorker
    member this.Format() =
        match this with
        | HttpCloudflareWorker -> "http/cloudflare-worker"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type cloudflareworkerrulepostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type cloudflareworkerrulepostTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of cloudflareworkerrulepostTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): cloudflareworkerrulepostTargetHeaders = { name = None; value = None }

type cloudflareworkerrulepostTarget =
    { ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<cloudflareworkerrulepostTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string>
      url: string }
    ///Creates an instance of cloudflareworkerrulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (url: string): cloudflareworkerrulepostTarget =
        { headers = None
          signingKeyId = None
          url = url }

type cloudflareworkerrulepost =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: cloudflareworkerrulepostRequestMode
      ///The type of rule. In this case Cloudflare Worker. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: cloudflareworkerrulepostRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<cloudflareworkerrulepostStatus>
      target: cloudflareworkerrulepostTarget }
    ///Creates an instance of cloudflareworkerrulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: cloudflareworkerrulepostRequestMode,
                          ruleType: cloudflareworkerrulepostRuleType,
                          source: rulesource,
                          target: cloudflareworkerrulepostTarget): cloudflareworkerrulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type cloudflareworkerruleresponseRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type cloudflareworkerruleresponseRuleType =
    | [<CompiledName "http/cloudflare-worker">] HttpCloudflareWorker
    member this.Format() =
        match this with
        | HttpCloudflareWorker -> "http/cloudflare-worker"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type cloudflareworkerruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type cloudflareworkerruleresponseTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of cloudflareworkerruleresponseTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): cloudflareworkerruleresponseTargetHeaders = { name = None; value = None }

type cloudflareworkerruleresponseTarget =
    { ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<cloudflareworkerruleresponseTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string>
      url: string }
    ///Creates an instance of cloudflareworkerruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (url: string): cloudflareworkerruleresponseTarget =
        { headers = None
          signingKeyId = None
          url = url }

type cloudflareworkerruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: cloudflareworkerruleresponseRequestMode
      ///The type of rule. In this case Cloudflare Worker. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: cloudflareworkerruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<cloudflareworkerruleresponseStatus>
      target: cloudflareworkerruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of cloudflareworkerruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: cloudflareworkerruleresponseRequestMode,
                          ruleType: cloudflareworkerruleresponseRuleType,
                          source: rulesource,
                          target: cloudflareworkerruleresponseTarget): cloudflareworkerruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

type error =
    { ///The HTTP status code returned.
      code: int
      ///Any additional details about the error message.
      details: Option<Newtonsoft.Json.Linq.JObject>
      ///The URL to documentation about the error code.
      href: string
      ///The error message.
      message: string
      ///The Ably error code.
      statusCode: int }
    ///Creates an instance of error with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (code: int, href: string, message: string, statusCode: int): error =
        { code = code
          details = None
          href = href
          message = message
          statusCode = statusCode }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionrulepatchRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionrulepatchRuleType =
    | [<CompiledName "http/google-cloud-function">] HttpGoogleCloudFunction
    member this.Format() =
        match this with
        | HttpGoogleCloudFunction -> "http/google-cloud-function"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionrulepatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionrulepatchTargetFormat =
    | [<CompiledName "json">] Json
    member this.Format() =
        match this with
        | Json -> "json"

type googlecloudfunctionrulepatchTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of googlecloudfunctionrulepatchTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): googlecloudfunctionrulepatchTargetHeaders = { name = None; value = None }

type googlecloudfunctionrulepatchTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a text-based encoding.
      format: Option<googlecloudfunctionrulepatchTargetFormat>
      ///The name of your Google Cloud Function.
      functionName: Option<string>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<googlecloudfunctionrulepatchTargetHeaders>>
      ///The project ID for your Google Cloud Project that was generated when you created your project.
      projectId: Option<string>
      ///The region in which your Google Cloud Function is hosted. See the &amp;lt;a href="https://cloud.google.com/compute/docs/regions-zones/"&amp;gt;Google documentation&amp;lt;/a&amp;gt; for more details.
      region: Option<string>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string> }
    ///Creates an instance of googlecloudfunctionrulepatchTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): googlecloudfunctionrulepatchTarget =
        { enveloped = None
          format = None
          functionName = None
          headers = None
          projectId = None
          region = None
          signingKeyId = None }

type googlecloudfunctionrulepatch =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: Option<googlecloudfunctionrulepatchRequestMode>
      ///The type of rule. In this case Google Cloud Function. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: Option<googlecloudfunctionrulepatchRuleType>
      source: Option<rulesource>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<googlecloudfunctionrulepatchStatus>
      target: Option<googlecloudfunctionrulepatchTarget> }
    ///Creates an instance of googlecloudfunctionrulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): googlecloudfunctionrulepatch =
        { requestMode = None
          ruleType = None
          source = None
          status = None
          target = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionrulepostRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionrulepostRuleType =
    | [<CompiledName "http/google-cloud-function">] HttpGoogleCloudFunction
    member this.Format() =
        match this with
        | HttpGoogleCloudFunction -> "http/google-cloud-function"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionrulepostTargetFormat =
    | [<CompiledName "json">] Json
    member this.Format() =
        match this with
        | Json -> "json"

type googlecloudfunctionrulepostTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of googlecloudfunctionrulepostTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): googlecloudfunctionrulepostTargetHeaders = { name = None; value = None }

type googlecloudfunctionrulepostTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a text-based encoding.
      format: Option<googlecloudfunctionrulepostTargetFormat>
      ///The name of your Google Cloud Function.
      functionName: string
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<googlecloudfunctionrulepostTargetHeaders>>
      ///The project ID for your Google Cloud Project that was generated when you created your project.
      projectId: string
      ///The region in which your Google Cloud Function is hosted. See the &amp;lt;a href="https://cloud.google.com/compute/docs/regions-zones/"&amp;gt;Google documentation&amp;lt;/a&amp;gt; for more details.
      region: string
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string> }
    ///Creates an instance of googlecloudfunctionrulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (functionName: string, projectId: string, region: string): googlecloudfunctionrulepostTarget =
        { enveloped = None
          format = None
          functionName = functionName
          headers = None
          projectId = projectId
          region = region
          signingKeyId = None }

type googlecloudfunctionrulepost =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: googlecloudfunctionrulepostRequestMode
      ///The type of rule. In this case Google Cloud Function. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: googlecloudfunctionrulepostRuleType
      source: rulesource
      target: googlecloudfunctionrulepostTarget }
    ///Creates an instance of googlecloudfunctionrulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: googlecloudfunctionrulepostRequestMode,
                          ruleType: googlecloudfunctionrulepostRuleType,
                          source: rulesource,
                          target: googlecloudfunctionrulepostTarget): googlecloudfunctionrulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionruleresponseRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionruleresponseRuleType =
    | [<CompiledName "http/google-cloud-function">] HttpGoogleCloudFunction
    member this.Format() =
        match this with
        | HttpGoogleCloudFunction -> "http/google-cloud-function"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type googlecloudfunctionruleresponseTargetFormat =
    | [<CompiledName "json">] Json
    member this.Format() =
        match this with
        | Json -> "json"

type googlecloudfunctionruleresponseTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of googlecloudfunctionruleresponseTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): googlecloudfunctionruleresponseTargetHeaders = { name = None; value = None }

type googlecloudfunctionruleresponseTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a text-based encoding.
      format: Option<googlecloudfunctionruleresponseTargetFormat>
      ///The name of your Google Cloud Function.
      functionName: string
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<googlecloudfunctionruleresponseTargetHeaders>>
      ///The project ID for your Google Cloud Project that was generated when you created your project.
      projectId: string
      ///The region in which your Google Cloud Function is hosted. See the &amp;lt;a href="https://cloud.google.com/compute/docs/regions-zones/"&amp;gt;Google documentation&amp;lt;/a&amp;gt; for more details.
      region: string
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string> }
    ///Creates an instance of googlecloudfunctionruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (functionName: string, projectId: string, region: string): googlecloudfunctionruleresponseTarget =
        { enveloped = None
          format = None
          functionName = functionName
          headers = None
          projectId = projectId
          region = region
          signingKeyId = None }

type googlecloudfunctionruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: googlecloudfunctionruleresponseRequestMode
      ///The type of rule. In this case Google Cloud Function. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: googlecloudfunctionruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<googlecloudfunctionruleresponseStatus>
      target: googlecloudfunctionruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of googlecloudfunctionruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: googlecloudfunctionruleresponseRequestMode,
                          ruleType: googlecloudfunctionruleresponseRuleType,
                          source: rulesource,
                          target: googlecloudfunctionruleresponseTarget): googlecloudfunctionruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httprulepatchRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httprulepatchRuleType =
    | [<CompiledName "http">] Http
    member this.Format() =
        match this with
        | Http -> "http"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httprulepatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httprulepatchTargetFormat =
    | [<CompiledName "json">] Json
    | [<CompiledName "msgpack">] Msgpack
    member this.Format() =
        match this with
        | Json -> "json"
        | Msgpack -> "msgpack"

type httprulepatchTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of httprulepatchTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): httprulepatchTargetHeaders = { name = None; value = None }

type httprulepatchTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a simpler text-based encoding, whereas MsgPack provides a more efficient binary encoding.
      format: Option<httprulepatchTargetFormat>
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<httprulepatchTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string>
      url: Option<string> }
    ///Creates an instance of httprulepatchTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): httprulepatchTarget =
        { enveloped = None
          format = None
          headers = None
          signingKeyId = None
          url = None }

type httprulepatch =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: Option<httprulepatchRequestMode>
      ///The type of rule. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: Option<httprulepatchRuleType>
      source: Option<rulesource>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<httprulepatchStatus>
      target: Option<httprulepatchTarget> }
    ///Creates an instance of httprulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): httprulepatch =
        { requestMode = None
          ruleType = None
          source = None
          status = None
          target = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httprulepostRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httprulepostRuleType =
    | [<CompiledName "http">] Http
    member this.Format() =
        match this with
        | Http -> "http"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httprulepostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httprulepostTargetFormat =
    | [<CompiledName "json">] Json
    | [<CompiledName "msgpack">] Msgpack
    member this.Format() =
        match this with
        | Json -> "json"
        | Msgpack -> "msgpack"

type httprulepostTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of httprulepostTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): httprulepostTargetHeaders = { name = None; value = None }

type httprulepostTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a simpler text-based encoding, whereas MsgPack provides a more efficient binary encoding.
      format: httprulepostTargetFormat
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<httprulepostTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string>
      ///The URL of the endpoint that is invoked when events occur on Ably.
      url: string }
    ///Creates an instance of httprulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (format: httprulepostTargetFormat, url: string): httprulepostTarget =
        { enveloped = None
          format = format
          headers = None
          signingKeyId = None
          url = url }

type httprulepost =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: httprulepostRequestMode
      ///The type of rule. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: httprulepostRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<httprulepostStatus>
      target: httprulepostTarget }
    ///Creates an instance of httprulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: httprulepostRequestMode,
                          ruleType: httprulepostRuleType,
                          source: rulesource,
                          target: httprulepostTarget): httprulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httpruleresponseRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httpruleresponseRuleType =
    | [<CompiledName "http">] Http
    member this.Format() =
        match this with
        | Http -> "http"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httpruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type httpruleresponseTargetFormat =
    | [<CompiledName "json">] Json
    | [<CompiledName "msgpack">] Msgpack
    member this.Format() =
        match this with
        | Json -> "json"
        | Msgpack -> "msgpack"

type httpruleresponseTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of httpruleresponseTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): httpruleresponseTargetHeaders = { name = None; value = None }

type httpruleresponseTarget =
    { ///Messages delivered through Reactor are wrapped in an Ably envelope by default that contains metadata about the message and its payload. The form of the envelope depends on whether it is part of a Webhook/Function or a Queue/Firehose rule. For everything besides Webhooks, you can ensure you only get the raw payload by unchecking "Enveloped" when setting up the rule.
      enveloped: Option<bool>
      ///JSON provides a simpler text-based encoding, whereas MsgPack provides a more efficient binary encoding.
      format: httpruleresponseTargetFormat
      ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<httpruleresponseTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string>
      url: string }
    ///Creates an instance of httpruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (format: httpruleresponseTargetFormat, url: string): httpruleresponseTarget =
        { enveloped = None
          format = format
          headers = None
          signingKeyId = None
          url = url }

type httpruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: httpruleresponseRequestMode
      ///The type of rule. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: httpruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<httpruleresponseStatus>
      target: httpruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of httpruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: httpruleresponseRequestMode,
                          ruleType: httpruleresponseRuleType,
                          source: rulesource,
                          target: httpruleresponseTarget): httpruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type iftttrulepostRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type iftttrulepostRuleType =
    | [<CompiledName "http/ifttt">] HttpIfttt
    member this.Format() =
        match this with
        | HttpIfttt -> "http/ifttt"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type iftttrulepostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type iftttrulepostTarget =
    { eventName: string
      webhookKey: string }
    ///Creates an instance of iftttrulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (eventName: string, webhookKey: string): iftttrulepostTarget =
        { eventName = eventName
          webhookKey = webhookKey }

type iftttrulepost =
    { ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: iftttrulepostRequestMode
      ///The type of rule. In this case IFTTT. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: iftttrulepostRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<iftttrulepostStatus>
      target: iftttrulepostTarget }
    ///Creates an instance of iftttrulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: iftttrulepostRequestMode,
                          ruleType: iftttrulepostRuleType,
                          source: rulesource,
                          target: iftttrulepostTarget): iftttrulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type iftttruleresponseRequestMode =
    | [<CompiledName "single">] Single
    member this.Format() =
        match this with
        | Single -> "single"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type iftttruleresponseRuleType =
    | [<CompiledName "http/ifttt">] HttpIfttt
    member this.Format() =
        match this with
        | HttpIfttt -> "http/ifttt"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type iftttruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type iftttruleresponseTarget =
    { eventName: string
      webhookKey: string }
    ///Creates an instance of iftttruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (eventName: string, webhookKey: string): iftttruleresponseTarget =
        { eventName = eventName
          webhookKey = webhookKey }

type iftttruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///Single request mode sends each event separately to the endpoint specified by the rule. You can read more about single request mode events in the &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      requestMode: iftttruleresponseRequestMode
      ///The type of rule. In this case IFTTT. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: iftttruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<iftttruleresponseStatus>
      target: iftttruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of iftttruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: iftttruleresponseRequestMode,
                          ruleType: iftttruleresponseRuleType,
                          source: rulesource,
                          target: iftttruleresponseTarget): iftttruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Capabilities =
    | [<CompiledName "publish">] Publish
    | [<CompiledName "subscribe">] Subscribe
    | [<CompiledName "history">] History
    | [<CompiledName "presence">] Presence
    | [<CompiledName "channel-metadata">] ChannelMetadata
    | [<CompiledName "push-admin">] PushAdmin
    | [<CompiledName "push-subscribe">] PushSubscribe
    | [<CompiledName "statistics">] Statistics
    member this.Format() =
        match this with
        | Publish -> "publish"
        | Subscribe -> "subscribe"
        | History -> "history"
        | Presence -> "presence"
        | ChannelMetadata -> "channel-metadata"
        | PushAdmin -> "push-admin"
        | PushSubscribe -> "push-subscribe"
        | Statistics -> "statistics"

type keypatch =
    { ///The capabilities that this key has. More information on capabilities can be found in the &amp;lt;a href="https://ably.com/documentation/core-features/authentication#capabilities-explained"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      capabilities: Option<list<Capabilities>>
      ///Specify the channels and queues that this key can be used with.
      channels: Option<string>
      ///The name for your API key. This is a friendly name for your reference.
      name: Option<string> }
    ///Creates an instance of keypatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): keypatch =
        { capabilities = None
          channels = None
          name = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type keypostCapabilities =
    | [<CompiledName "publish">] Publish
    | [<CompiledName "subscribe">] Subscribe
    | [<CompiledName "history">] History
    | [<CompiledName "presence">] Presence
    | [<CompiledName "channel-metadata">] ChannelMetadata
    | [<CompiledName "push-admin">] PushAdmin
    | [<CompiledName "push-subscribe">] PushSubscribe
    | [<CompiledName "statistics">] Statistics
    member this.Format() =
        match this with
        | Publish -> "publish"
        | Subscribe -> "subscribe"
        | History -> "history"
        | Presence -> "presence"
        | ChannelMetadata -> "channel-metadata"
        | PushAdmin -> "push-admin"
        | PushSubscribe -> "push-subscribe"
        | Statistics -> "statistics"

type keypost =
    { ///The capabilities that this key has. More information on capabilities can be found in the &amp;lt;a href="https://ably.com/documentation/core-features/authentication#capabilities-explained"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      capabilities: list<keypostCapabilities>
      ///Specify the channels and queues that this key can be used with.
      channels: string
      ///The name for your API key. This is a friendly name for your reference.
      name: string }
    ///Creates an instance of keypost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (capabilities: list<keypostCapabilities>, channels: string, name: string): keypost =
        { capabilities = capabilities
          channels = channels
          name = name }

type keyresponse =
    { ///The Ably application ID which this key is associated with.
      appId: Option<string>
      ///The capabilities that this key has. More information on capabilities can be found in the &amp;lt;a href="https://ably.com/documentation/core-features/authentication#capabilities-explained"&amp;gt;Ably documentation&amp;lt;/a&amp;gt;.
      capability: Option<Map<string, list<string>>>
      ///Unix timestamp representing the date and time of creation of the key.
      created: Option<int>
      ///The key ID.
      id: Option<string>
      ///The complete API key including API secret.
      key: Option<string>
      ///Unix timestamp representing the date and time of the last modification of the key.
      modified: Option<int>
      ///The name of the application this key is associated with.
      name: Option<string> }
    ///Creates an instance of keyresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): keyresponse =
        { appId = None
          capability = None
          created = None
          id = None
          key = None
          modified = None
          name = None }

type Account =
    { ///The account ID.
      id: string
      ///The name of the account.
      name: string }
    ///Creates an instance of Account with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string, name: string): Account = { id = id; name = name }

type Token =
    { ///An array containing the access capabilities associated with the access token.
      capabilities: list<string>
      ///The token ID. This is a UUID.
      id: int
      ///The friendly name for the token.
      name: string }
    ///Creates an instance of Token with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (capabilities: list<string>, id: int, name: string): Token =
        { capabilities = capabilities
          id = id
          name = name }

type User =
    { ///Email address of the user associated with the account.
      email: string
      ///The user ID associated with the account. This is a UUID.
      id: int }
    ///Creates an instance of User with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (email: string, id: int): User = { email = email; id = id }

type me =
    { account: Option<Account>
      token: Option<Token>
      user: Option<User> }
    ///Creates an instance of me with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): me =
        { account = None
          token = None
          user = None }

type namespacepatch =
    { ///If `true`, clients will not be permitted to use (including to attach, publish, or subscribe) any channels within this namespace unless they are identified, that is, authenticated using a client ID. See the &amp;lt;a href="https://knowledge.ably.com/authenticated-and-identified-clients"&amp;gt;Ably knowledge base/a&amp;gt; for more details.
      authenticated: Option<bool>
      ///If `true`, the last message published on a channel will be stored for 365 days. You can access the stored message only by using the channel rewind mechanism and attaching with rewind=1. Please note that for each message stored, an additional message is deducted from your monthly allocation.
      persistLast: Option<bool>
      ///If `true`, all messages on a channel will be stored for 24 hours. You can access stored messages via the History API. Please note that for each message stored, an additional message is deducted from your monthly allocation.
      persisted: Option<bool>
      ///If `true`, publishing messages with a push payload in the extras field is permitted and can trigger the delivery of a native push notification to registered devices for the channel.
      pushEnabled: Option<bool>
      ///If `true`, only clients that are connected using TLS will be permitted to subscribe to any channels within this namespace.
      tlsOnly: Option<bool> }
    ///Creates an instance of namespacepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): namespacepatch =
        { authenticated = None
          persistLast = None
          persisted = None
          pushEnabled = None
          tlsOnly = None }

type namespacepost =
    { ///If `true`, clients will not be permitted to use (including to attach, publish, or subscribe) any channels within this namespace unless they are identified, that is, authenticated using a client ID. See the &amp;lt;a href="https://knowledge.ably.com/authenticated-and-identified-clients"&amp;gt;Ably Knowledge base&amp;lt;/a&amp;gt; for more details.
      authenticated: Option<bool>
      ///The namespace or channel name that the channel rule will apply to. For example, if you specify `namespace` the namespace will be set to `namespace` and will match with channels `namespace:*` and `namespace`.
      id: string
      ///If `true`, the last message published on a channel will be stored for 365 days. You can access the stored message only by using the channel rewind mechanism and attaching with rewind=1. Please note that for each message stored, an additional message is deducted from your monthly allocation.
      persistLast: Option<bool>
      ///If `true`, all messages on a channel will be stored for 24 hours. You can access stored messages via the History API. Please note that for each message stored, an additional message is deducted from your monthly allocation.
      persisted: Option<bool>
      ///If `true`, publishing messages with a push payload in the extras field is permitted and can trigger the delivery of a native push notification to registered devices for the channel.
      pushEnabled: Option<bool>
      ///If `true`, only clients that are connected using TLS will be permitted to subscribe to any channels within this namespace.
      tlsOnly: Option<bool> }
    ///Creates an instance of namespacepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (id: string): namespacepost =
        { authenticated = None
          id = id
          persistLast = None
          persisted = None
          pushEnabled = None
          tlsOnly = None }

type namespaceresponse =
    { ///If `true`, clients will not be permitted to use (including to attach, publish, or subscribe) any channels within this namespace unless they are identified, that is, authenticated using a client ID. See the &amp;lt;a href="https://knowledge.ably.com/authenticated-and-identified-clients"&amp;gt;Ably knowledge base&amp;lt;/a&amp;gt; for more details.
      authenticated: Option<bool>
      ///Unix timestamp representing the date and time of creation of the namespace.
      created: Option<int>
      ///The namespace or channel name that the channel rule will apply to. For example, if you specify `namespace` the namespace will be set to `namespace` and will match with channels `namespace:*` and `namespace`.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the namespace.
      modified: Option<int>
      ///If `true`, the last message published on a channel will be stored for 365 days. You can access the stored message only by using the channel rewind mechanism and attaching with rewind=1. Please note that for each message stored, an additional message is deducted from your monthly allocation.
      persistLast: Option<bool>
      ///If `true`, all messages on a channel will be stored for 24 hours. You can access stored messages via the History API. Please note that for each message stored, an additional message is deducted from your monthly allocation.
      persisted: Option<bool>
      ///If `true`, publishing messages with a push payload in the extras field is permitted and can trigger the delivery of a native push notification to registered devices for the channel.
      pushEnabled: Option<bool>
      ///If `true`, only clients that are connected using TLS will be permitted to subscribe to any channels within this namespace.
      tlsOnly: Option<bool> }
    ///Creates an instance of namespaceresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): namespaceresponse =
        { authenticated = None
          created = None
          id = None
          modified = None
          persistLast = None
          persisted = None
          pushEnabled = None
          tlsOnly = None }

type queue =
    { ///Message limit in number of messages.
      maxLength: int
      ///A friendly name for your queue.
      name: string
      ///The data center region. US East (Virginia) or EU West (Ireland). Values are `us-east-1-a` or `eu-west-1-a`.
      region: string
      ///TTL in minutes.
      ttl: int }
    ///Creates an instance of queue with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (maxLength: int, name: string, region: string, ttl: int): queue =
        { maxLength = maxLength
          name = name
          region = region
          ttl = ttl }

type Amqp =
    { ///Name of the Ably queue.
      queueName: Option<string>
      ///URI for the AMQP queue interface.
      uri: Option<string> }
    ///Creates an instance of Amqp with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Amqp = { queueName = None; uri = None }

///Details of messages in the queue.
type Messages =
    { ///The number of ready messages in the queue.
      ready: Option<int>
      ///The total number of messages in the queue.
      total: Option<int>
      ///The number of unacknowledged messages in the queue.
      unacknowledged: Option<int> }
    ///Creates an instance of Messages with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Messages =
        { ready = None
          total = None
          unacknowledged = None }

type Stats =
    { ///The rate at which messages are acknowledged. Rate is messages per minute.
      acknowledgementRate: Option<float>
      ///The rate at which messages are delivered from the queue. Rate is messages per minute.
      deliveryRate: Option<float>
      ///The rate at which messages are published to the queue. Rate is messages per minute.
      publishRate: Option<float> }
    ///Creates an instance of Stats with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Stats =
        { acknowledgementRate = None
          deliveryRate = None
          publishRate = None }

type Stomp =
    { ///Destination queue.
      destination: Option<string>
      ///The host type for the queue.
      host: Option<string>
      ///URI for the STOMP queue interface.
      uri: Option<string> }
    ///Creates an instance of Stomp with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): Stomp =
        { destination = None
          host = None
          uri = None }

type queueresponse =
    { amqp: Option<Amqp>
      ///The Ably application ID.
      appId: Option<string>
      ///A boolean that indicates whether this is a dead letter queue or not.
      deadletter: Option<bool>
      deadletterId: Option<string>
      ///The ID of the Ably queue
      id: Option<string>
      ///Message limit in number of messages.
      maxLength: Option<int>
      ///Details of messages in the queue.
      messages: Option<Messages>
      ///The friendly name of the queue.
      name: Option<string>
      ///The data center region for the queue.
      region: Option<string>
      ///The current state of the queue.
      state: Option<string>
      stats: Option<Stats>
      stomp: Option<Stomp>
      ///TTL in minutes.
      ttl: Option<int> }
    ///Creates an instance of queueresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): queueresponse =
        { amqp = None
          appId = None
          deadletter = None
          deadletterId = None
          id = None
          maxLength = None
          messages = None
          name = None
          region = None
          state = None
          stats = None
          stomp = None
          ttl = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type ruleattributesStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type ruleattributes =
    { ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<ruleattributesStatus>
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of ruleattributes with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): ruleattributes =
        { appId = None
          created = None
          id = None
          modified = None
          status = None
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type Type =
    | [<CompiledName "channel.message">] ChannelMessage
    | [<CompiledName "channel.presence">] ChannelPresence
    | [<CompiledName "channel.lifecycle">] ChannelLifecycle
    | [<CompiledName "channel.occupancy">] ChannelOccupancy
    member this.Format() =
        match this with
        | ChannelMessage -> "channel.message"
        | ChannelPresence -> "channel.presence"
        | ChannelLifecycle -> "channel.lifecycle"
        | ChannelOccupancy -> "channel.occupancy"

type rulesource =
    { ///This field allows you to filter your rule based on a regular expression that is matched against the complete channel name. Leave this empty if you want the rule to apply to all channels.
      channelFilter: string
      ///The type `channel.message` delivers all messages published on a channel. The type `channel.presence` delivers all enter, update and leave events for members present on a channel. The type `channel.lifecycle` events for this rule type are currently not supported. Get in touch (https://ably.com/contact) if you need this feature. The type `channel.occupancy` delivers all occupancy events for the channel.
      ``type``: Type }
    ///Creates an instance of rulesource with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (channelFilter: string, ``type``: Type): rulesource =
        { channelFilter = channelFilter
          ``type`` = ``type`` }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type rulesourcepatchType =
    | [<CompiledName "channel.message">] ChannelMessage
    | [<CompiledName "channel.presence">] ChannelPresence
    | [<CompiledName "channel.lifecycle">] ChannelLifecycle
    | [<CompiledName "channel.occupancy">] ChannelOccupancy
    member this.Format() =
        match this with
        | ChannelMessage -> "channel.message"
        | ChannelPresence -> "channel.presence"
        | ChannelLifecycle -> "channel.lifecycle"
        | ChannelOccupancy -> "channel.occupancy"

type rulesourcepatch =
    { ///This field allows you to filter your rule based on a regular expression that is matched against the complete channel name. Leave this empty if you want the rule to apply to all channels.
      channelFilter: Option<string>
      ///The type `channel.message` delivers all messages published on a channel. The type `channel.presence` delivers all enter, update and leave events for members present on a channel. The type `channel.lifecycle` events for this rule type are currently not supported. Get in touch (https://ably.com/contact) if you need this feature. The type `channel.occupancy` delivers all occupancy events for the channel.
      ``type``: Option<rulesourcepatchType> }
    ///Creates an instance of rulesourcepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): rulesourcepatch =
        { channelFilter = None
          ``type`` = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type unsupportedruleresponseRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type unsupportedruleresponseRuleType =
    | [<CompiledName "unsupported">] Unsupported
    member this.Format() =
        match this with
        | Unsupported -> "unsupported"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type unsupportedruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type unsupportedruleresponseTarget =
    { url: string }
    ///Creates an instance of unsupportedruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (url: string): unsupportedruleresponseTarget = { url = url }

type unsupportedruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: unsupportedruleresponseRequestMode
      ///This rule type is currently unsupported.
      ruleType: unsupportedruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<unsupportedruleresponseStatus>
      target: unsupportedruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of unsupportedruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: unsupportedruleresponseRequestMode,
                          ruleType: unsupportedruleresponseRuleType,
                          source: rulesource,
                          target: unsupportedruleresponseTarget): unsupportedruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type zapierrulepatchRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type zapierrulepatchRuleType =
    | [<CompiledName "http/zapier">] HttpZapier
    member this.Format() =
        match this with
        | HttpZapier -> "http/zapier"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type zapierrulepatchStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type zapierrulepatchTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of zapierrulepatchTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): zapierrulepatchTargetHeaders = { name = None; value = None }

type zapierrulepatchTarget =
    { ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<zapierrulepatchTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string>
      url: Option<string> }
    ///Creates an instance of zapierrulepatchTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): zapierrulepatchTarget =
        { headers = None
          signingKeyId = None
          url = None }

type zapierrulepatch =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: Option<zapierrulepatchRequestMode>
      ///The type of rule. In this case Zapier. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: Option<zapierrulepatchRuleType>
      source: Option<rulesource>
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<zapierrulepatchStatus>
      target: Option<zapierrulepatchTarget> }
    ///Creates an instance of zapierrulepatch with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): zapierrulepatch =
        { requestMode = None
          ruleType = None
          source = None
          status = None
          target = None }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type zapierrulepostRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type zapierrulepostRuleType =
    | [<CompiledName "http/zapier">] HttpZapier
    member this.Format() =
        match this with
        | HttpZapier -> "http/zapier"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type zapierrulepostStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type zapierrulepostTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of zapierrulepostTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): zapierrulepostTargetHeaders = { name = None; value = None }

type zapierrulepostTarget =
    { ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<zapierrulepostTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string>
      url: string }
    ///Creates an instance of zapierrulepostTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (url: string): zapierrulepostTarget =
        { headers = None
          signingKeyId = None
          url = url }

type zapierrulepost =
    { ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: zapierrulepostRequestMode
      ///The type of rule. In this case Zapier. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: zapierrulepostRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<zapierrulepostStatus>
      target: zapierrulepostTarget }
    ///Creates an instance of zapierrulepost with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: zapierrulepostRequestMode,
                          ruleType: zapierrulepostRuleType,
                          source: rulesource,
                          target: zapierrulepostTarget): zapierrulepost =
        { requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target }

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type zapierruleresponseRequestMode =
    | [<CompiledName "single">] Single
    | [<CompiledName "batch">] Batch
    member this.Format() =
        match this with
        | Single -> "single"
        | Batch -> "batch"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type zapierruleresponseRuleType =
    | [<CompiledName "http/zapier">] HttpZapier
    member this.Format() =
        match this with
        | HttpZapier -> "http/zapier"

[<Fable.Core.StringEnum; RequireQualifiedAccess>]
type zapierruleresponseStatus =
    | [<CompiledName "enabled">] Enabled
    | [<CompiledName "disabled">] Disabled
    member this.Format() =
        match this with
        | Enabled -> "enabled"
        | Disabled -> "disabled"

type zapierruleresponseTargetHeaders =
    { ///The name of the header.
      name: Option<string>
      ///The value of the header.
      value: Option<string> }
    ///Creates an instance of zapierruleresponseTargetHeaders with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (): zapierruleresponseTargetHeaders = { name = None; value = None }

type zapierruleresponseTarget =
    { ///If you have additional information to send, you'll need to include the relevant headers.
      headers: Option<list<zapierruleresponseTargetHeaders>>
      ///The signing key ID for use in `batch` mode. Ably will optionally sign the payload using an API key ensuring your servers can validate the payload using the private API key. See the &amp;lt;a href="https://ably.com/documentation/general/events#security"&amp;gt;webhook security docs&amp;lt;/a&amp;gt; for more information.
      signingKeyId: Option<string>
      url: string }
    ///Creates an instance of zapierruleresponseTarget with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (url: string): zapierruleresponseTarget =
        { headers = None
          signingKeyId = None
          url = url }

type zapierruleresponse =
    { _links: Option<Newtonsoft.Json.Linq.JObject>
      ///The Ably application ID.
      appId: Option<string>
      ///Unix timestamp representing the date and time of creation of the rule.
      created: Option<float>
      ///The rule ID.
      id: Option<string>
      ///Unix timestamp representing the date and time of last modification of the rule.
      modified: Option<float>
      ///This is Single Request mode or Batch Request mode. Single Request mode sends each event separately to the endpoint specified by the rule. Batch Request mode rolls up multiple events into the same request. You can read more about the difference between single and batched events in the Ably &amp;lt;a href="https://ably.com/documentation/general/events#batching"&amp;gt;documentation&amp;lt;/a&amp;gt;.
      requestMode: zapierruleresponseRequestMode
      ///The type of rule. In this case Zapier. See the &amp;lt;a href="https://ably.com/integrations"&amp;gt;documentation&amp;lt;/a&amp;gt; for further information.
      ruleType: zapierruleresponseRuleType
      source: rulesource
      ///The status of the rule. Rules can be enabled or disabled.
      status: Option<zapierruleresponseStatus>
      target: zapierruleresponseTarget
      ///API version. Events and the format of their payloads are versioned. Please see the &amp;lt;a href="https://ably.com/documentation/general/events"&amp;gt;Events documentation&amp;lt;/a&amp;gt;.
      version: Option<string> }
    ///Creates an instance of zapierruleresponse with all optional fields initialized to None. The required fields are parameters of this function
    static member Create (requestMode: zapierruleresponseRequestMode,
                          ruleType: zapierruleresponseRuleType,
                          source: rulesource,
                          target: zapierruleresponseTarget): zapierruleresponse =
        { _links = None
          appId = None
          created = None
          id = None
          modified = None
          requestMode = requestMode
          ruleType = ruleType
          source = source
          status = None
          target = target
          version = None }

[<RequireQualifiedAccess>]
type GetAccountsAppsByAccountId =
    ///List of apps for the specified account are returned
    | OK of payload: list<appresponse>
    ///Authentication failed
    | Unauthorized of payload: error
    ///Account not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PostAccountsAppsByAccountId =
    ///App created
    | Created of payload: appresponse
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///Account not found
    | NotFound of payload: error
    ///Invalid request
    | UnprocessableEntity of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type GetAppsKeysByAppId =
    ///Key list
    | OK of payload: list<keyresponse>
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PostAppsKeysByAppId =
    ///Key created
    | Created of payload: keyresponse
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Invalid request
    | UnprocessableEntity of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PatchAppsKeysByAppIdAndKeyId =
    ///Key updated
    | OK of payload: keyresponse
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Invalid request
    | UnprocessableEntity of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PostAppsKeysRevokeByAppIdAndKeyId =
    ///Key revoked
    | OK
    ///Authentication failed
    | Unauthorized of payload: error
    ///Not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type GetAppsNamespacesByAppId =
    ///Namespace list
    | OK of payload: list<namespaceresponse>
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PostAppsNamespacesByAppId =
    ///Namespace created
    | Created of payload: namespaceresponse
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Invalid request
    | UnprocessableEntity of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type DeleteAppsNamespacesByAppIdAndNamespaceId =
    ///Namespace deleted
    | NoContent
    ///Authentication failed
    | Unauthorized of payload: error
    ///Not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PatchAppsNamespacesByAppIdAndNamespaceId =
    ///Namespace updated
    | OK of payload: namespaceresponse
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///Not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type GetAppsQueuesByAppId =
    ///Queue list
    | OK of payload: list<queueresponse>
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error
    ///503 Service unavailable
    | ServiceUnavailable of payload: error

[<RequireQualifiedAccess>]
type PostAppsQueuesByAppId =
    ///Queue created
    | Created of payload: queueresponse
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Invalid request
    | UnprocessableEntity of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type DeleteAppsQueuesByAppIdAndQueueId =
    ///Queue deleted
    | NoContent
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error
    ///503 Service unavailable
    | ServiceUnavailable of payload: error

[<RequireQualifiedAccess>]
type GetAppsRulesByAppId =
    ///Reactor rule list
    | OK of payload: list<ruleresponse>
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PostAppsRulesByAppId =
    ///Reactor rule created
    | Created
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Invalid request
    | UnprocessableEntity of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type DeleteAppsRulesByAppIdAndRuleId =
    ///Reactor rule deleted
    | NoContent
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type GetAppsRulesByAppIdAndRuleId =
    ///Reactor rule
    | OK
    ///Authentication failed
    | Unauthorized of payload: error
    ///Not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PatchAppsRulesByAppIdAndRuleId =
    ///Reactor rule updated
    | OK
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Invalid request
    | UnprocessableEntity of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type DeleteAppsById =
    ///App deleted
    | NoContent
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Invalid request
    | UnprocessableEntity of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PatchAppsById =
    ///App updated
    | OK of payload: appresponse
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type PostAppsPkcs12ById =
    ///App updated
    | OK of payload: appresponse
    ///Bad request
    | BadRequest of payload: error
    ///Authentication failed
    | Unauthorized of payload: error
    ///App not found
    | NotFound of payload: error
    ///Internal server error
    | InternalServerError of payload: error

[<RequireQualifiedAccess>]
type GetMe =
    ///Token details
    | OK of payload: me
    ///Authentication failed
    | Unauthorized of payload: error
    ///Internal server error
    | InternalServerError of payload: error
