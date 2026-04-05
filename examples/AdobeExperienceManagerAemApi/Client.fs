namespace rec AdobeExperienceManagerAemApi

open System.Net
open System.Net.Http
open System.Text
open System.Threading
open AdobeExperienceManagerAemApi.Types
open AdobeExperienceManagerAemApi.Http

///Swagger AEM is an OpenAPI specification for Adobe Experience Manager (AEM) API
type AdobeExperienceManagerAemApiClient(httpClient: HttpClient) =
    member this.PostCqActions(authorizableId: string, changelog: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.query ("authorizableId", authorizableId)
                  RequestPart.query ("changelog", changelog) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/.cqactions.html" requestParts cancellationToken
            return PostCqActions.DefaultResponse
        }

    member this.PostConfigAdobeGraniteSamlAuthenticationHandler
        (
            ?keyStorePassword: string,
            ?keyStorePasswordTypeHint: string,
            ?serviceRanking: int,
            ?serviceRankingTypeHint: string,
            ?idpHttpRedirect: bool,
            ?idpHttpRedirectTypeHint: string,
            ?createUser: bool,
            ?createUserTypeHint: string,
            ?defaultRedirectUrl: string,
            ?defaultRedirectUrlTypeHint: string,
            ?userIDAttribute: string,
            ?userIDAttributeTypeHint: string,
            ?defaultGroups: list<string>,
            ?defaultGroupsTypeHint: string,
            ?idpCertAlias: string,
            ?idpCertAliasTypeHint: string,
            ?addGroupMemberships: bool,
            ?addGroupMembershipsTypeHint: string,
            ?path: list<string>,
            ?pathTypeHint: string,
            ?synchronizeAttributes: list<string>,
            ?synchronizeAttributesTypeHint: string,
            ?clockTolerance: int,
            ?clockToleranceTypeHint: string,
            ?groupMembershipAttribute: string,
            ?groupMembershipAttributeTypeHint: string,
            ?idpUrl: string,
            ?idpUrlTypeHint: string,
            ?logoutUrl: string,
            ?logoutUrlTypeHint: string,
            ?serviceProviderEntityId: string,
            ?serviceProviderEntityIdTypeHint: string,
            ?assertionConsumerServiceURL: string,
            ?assertionConsumerServiceURLTypeHint: string,
            ?handleLogout: bool,
            ?handleLogoutTypeHint: string,
            ?spPrivateKeyAlias: string,
            ?spPrivateKeyAliasTypeHint: string,
            ?useEncryption: bool,
            ?useEncryptionTypeHint: string,
            ?nameIdFormat: string,
            ?nameIdFormatTypeHint: string,
            ?digestMethod: string,
            ?digestMethodTypeHint: string,
            ?signatureMethod: string,
            ?signatureMethodTypeHint: string,
            ?userIntermediatePath: string,
            ?userIntermediatePathTypeHint: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if keyStorePassword.IsSome then
                      RequestPart.query ("keyStorePassword", keyStorePassword.Value)
                  if keyStorePasswordTypeHint.IsSome then
                      RequestPart.query ("keyStorePassword@TypeHint", keyStorePasswordTypeHint.Value)
                  if serviceRanking.IsSome then
                      RequestPart.query ("service.ranking", serviceRanking.Value)
                  if serviceRankingTypeHint.IsSome then
                      RequestPart.query ("service.ranking@TypeHint", serviceRankingTypeHint.Value)
                  if idpHttpRedirect.IsSome then
                      RequestPart.query ("idpHttpRedirect", idpHttpRedirect.Value)
                  if idpHttpRedirectTypeHint.IsSome then
                      RequestPart.query ("idpHttpRedirect@TypeHint", idpHttpRedirectTypeHint.Value)
                  if createUser.IsSome then
                      RequestPart.query ("createUser", createUser.Value)
                  if createUserTypeHint.IsSome then
                      RequestPart.query ("createUser@TypeHint", createUserTypeHint.Value)
                  if defaultRedirectUrl.IsSome then
                      RequestPart.query ("defaultRedirectUrl", defaultRedirectUrl.Value)
                  if defaultRedirectUrlTypeHint.IsSome then
                      RequestPart.query ("defaultRedirectUrl@TypeHint", defaultRedirectUrlTypeHint.Value)
                  if userIDAttribute.IsSome then
                      RequestPart.query ("userIDAttribute", userIDAttribute.Value)
                  if userIDAttributeTypeHint.IsSome then
                      RequestPart.query ("userIDAttribute@TypeHint", userIDAttributeTypeHint.Value)
                  if defaultGroups.IsSome then
                      RequestPart.query ("defaultGroups", defaultGroups.Value)
                  if defaultGroupsTypeHint.IsSome then
                      RequestPart.query ("defaultGroups@TypeHint", defaultGroupsTypeHint.Value)
                  if idpCertAlias.IsSome then
                      RequestPart.query ("idpCertAlias", idpCertAlias.Value)
                  if idpCertAliasTypeHint.IsSome then
                      RequestPart.query ("idpCertAlias@TypeHint", idpCertAliasTypeHint.Value)
                  if addGroupMemberships.IsSome then
                      RequestPart.query ("addGroupMemberships", addGroupMemberships.Value)
                  if addGroupMembershipsTypeHint.IsSome then
                      RequestPart.query ("addGroupMemberships@TypeHint", addGroupMembershipsTypeHint.Value)
                  if path.IsSome then
                      RequestPart.query ("path", path.Value)
                  if pathTypeHint.IsSome then
                      RequestPart.query ("path@TypeHint", pathTypeHint.Value)
                  if synchronizeAttributes.IsSome then
                      RequestPart.query ("synchronizeAttributes", synchronizeAttributes.Value)
                  if synchronizeAttributesTypeHint.IsSome then
                      RequestPart.query ("synchronizeAttributes@TypeHint", synchronizeAttributesTypeHint.Value)
                  if clockTolerance.IsSome then
                      RequestPart.query ("clockTolerance", clockTolerance.Value)
                  if clockToleranceTypeHint.IsSome then
                      RequestPart.query ("clockTolerance@TypeHint", clockToleranceTypeHint.Value)
                  if groupMembershipAttribute.IsSome then
                      RequestPart.query ("groupMembershipAttribute", groupMembershipAttribute.Value)
                  if groupMembershipAttributeTypeHint.IsSome then
                      RequestPart.query ("groupMembershipAttribute@TypeHint", groupMembershipAttributeTypeHint.Value)
                  if idpUrl.IsSome then
                      RequestPart.query ("idpUrl", idpUrl.Value)
                  if idpUrlTypeHint.IsSome then
                      RequestPart.query ("idpUrl@TypeHint", idpUrlTypeHint.Value)
                  if logoutUrl.IsSome then
                      RequestPart.query ("logoutUrl", logoutUrl.Value)
                  if logoutUrlTypeHint.IsSome then
                      RequestPart.query ("logoutUrl@TypeHint", logoutUrlTypeHint.Value)
                  if serviceProviderEntityId.IsSome then
                      RequestPart.query ("serviceProviderEntityId", serviceProviderEntityId.Value)
                  if serviceProviderEntityIdTypeHint.IsSome then
                      RequestPart.query ("serviceProviderEntityId@TypeHint", serviceProviderEntityIdTypeHint.Value)
                  if assertionConsumerServiceURL.IsSome then
                      RequestPart.query ("assertionConsumerServiceURL", assertionConsumerServiceURL.Value)
                  if assertionConsumerServiceURLTypeHint.IsSome then
                      RequestPart.query (
                          "assertionConsumerServiceURL@TypeHint",
                          assertionConsumerServiceURLTypeHint.Value
                      )
                  if handleLogout.IsSome then
                      RequestPart.query ("handleLogout", handleLogout.Value)
                  if handleLogoutTypeHint.IsSome then
                      RequestPart.query ("handleLogout@TypeHint", handleLogoutTypeHint.Value)
                  if spPrivateKeyAlias.IsSome then
                      RequestPart.query ("spPrivateKeyAlias", spPrivateKeyAlias.Value)
                  if spPrivateKeyAliasTypeHint.IsSome then
                      RequestPart.query ("spPrivateKeyAlias@TypeHint", spPrivateKeyAliasTypeHint.Value)
                  if useEncryption.IsSome then
                      RequestPart.query ("useEncryption", useEncryption.Value)
                  if useEncryptionTypeHint.IsSome then
                      RequestPart.query ("useEncryption@TypeHint", useEncryptionTypeHint.Value)
                  if nameIdFormat.IsSome then
                      RequestPart.query ("nameIdFormat", nameIdFormat.Value)
                  if nameIdFormatTypeHint.IsSome then
                      RequestPart.query ("nameIdFormat@TypeHint", nameIdFormatTypeHint.Value)
                  if digestMethod.IsSome then
                      RequestPart.query ("digestMethod", digestMethod.Value)
                  if digestMethodTypeHint.IsSome then
                      RequestPart.query ("digestMethod@TypeHint", digestMethodTypeHint.Value)
                  if signatureMethod.IsSome then
                      RequestPart.query ("signatureMethod", signatureMethod.Value)
                  if signatureMethodTypeHint.IsSome then
                      RequestPart.query ("signatureMethod@TypeHint", signatureMethodTypeHint.Value)
                  if userIntermediatePath.IsSome then
                      RequestPart.query ("userIntermediatePath", userIntermediatePath.Value)
                  if userIntermediatePathTypeHint.IsSome then
                      RequestPart.query ("userIntermediatePath@TypeHint", userIntermediatePathTypeHint.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/apps/system/config/com.adobe.granite.auth.saml.SamlAuthenticationHandler.config"
                    requestParts
                    cancellationToken

            return PostConfigAdobeGraniteSamlAuthenticationHandler.DefaultResponse
        }

    member this.PostConfigAemPasswordReset
        (
            ?pwdresetAuthorizables: list<string>,
            ?pwdresetAuthorizablesTypeHint: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if pwdresetAuthorizables.IsSome then
                      RequestPart.query ("pwdreset.authorizables", pwdresetAuthorizables.Value)
                  if pwdresetAuthorizablesTypeHint.IsSome then
                      RequestPart.query ("pwdreset.authorizables@TypeHint", pwdresetAuthorizablesTypeHint.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/apps/system/config/com.shinesolutions.aem.passwordreset.Activator"
                    requestParts
                    cancellationToken

            return PostConfigAemPasswordReset.DefaultResponse
        }

    member this.PostConfigAemHealthCheckServlet
        (
            ?bundlesIgnored: list<string>,
            ?bundlesIgnoredTypeHint: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if bundlesIgnored.IsSome then
                      RequestPart.query ("bundles.ignored", bundlesIgnored.Value)
                  if bundlesIgnoredTypeHint.IsSome then
                      RequestPart.query ("bundles.ignored@TypeHint", bundlesIgnoredTypeHint.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/apps/system/config/com.shinesolutions.healthcheck.hc.impl.ActiveBundleHealthCheck"
                    requestParts
                    cancellationToken

            return PostConfigAemHealthCheckServlet.DefaultResponse
        }

    member this.PostConfigApacheFelixJettyBasedHttpService
        (
            ?orgApacheFelixHttpsNio: bool,
            ?orgApacheFelixHttpsNioTypeHint: string,
            ?orgApacheFelixHttpsKeystore: string,
            ?orgApacheFelixHttpsKeystoreTypeHint: string,
            ?orgApacheFelixHttpsKeystorePassword: string,
            ?orgApacheFelixHttpsKeystorePasswordTypeHint: string,
            ?orgApacheFelixHttpsKeystoreKey: string,
            ?orgApacheFelixHttpsKeystoreKeyTypeHint: string,
            ?orgApacheFelixHttpsKeystoreKeyPassword: string,
            ?orgApacheFelixHttpsKeystoreKeyPasswordTypeHint: string,
            ?orgApacheFelixHttpsTruststore: string,
            ?orgApacheFelixHttpsTruststoreTypeHint: string,
            ?orgApacheFelixHttpsTruststorePassword: string,
            ?orgApacheFelixHttpsTruststorePasswordTypeHint: string,
            ?orgApacheFelixHttpsClientcertificate: string,
            ?orgApacheFelixHttpsClientcertificateTypeHint: string,
            ?orgApacheFelixHttpsEnable: bool,
            ?orgApacheFelixHttpsEnableTypeHint: string,
            ?orgOsgiServiceHttpPortSecure: string,
            ?orgOsgiServiceHttpPortSecureTypeHint: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if orgApacheFelixHttpsNio.IsSome then
                      RequestPart.query ("org.apache.felix.https.nio", orgApacheFelixHttpsNio.Value)
                  if orgApacheFelixHttpsNioTypeHint.IsSome then
                      RequestPart.query ("org.apache.felix.https.nio@TypeHint", orgApacheFelixHttpsNioTypeHint.Value)
                  if orgApacheFelixHttpsKeystore.IsSome then
                      RequestPart.query ("org.apache.felix.https.keystore", orgApacheFelixHttpsKeystore.Value)
                  if orgApacheFelixHttpsKeystoreTypeHint.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.keystore@TypeHint",
                          orgApacheFelixHttpsKeystoreTypeHint.Value
                      )
                  if orgApacheFelixHttpsKeystorePassword.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.keystore.password",
                          orgApacheFelixHttpsKeystorePassword.Value
                      )
                  if orgApacheFelixHttpsKeystorePasswordTypeHint.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.keystore.password@TypeHint",
                          orgApacheFelixHttpsKeystorePasswordTypeHint.Value
                      )
                  if orgApacheFelixHttpsKeystoreKey.IsSome then
                      RequestPart.query ("org.apache.felix.https.keystore.key", orgApacheFelixHttpsKeystoreKey.Value)
                  if orgApacheFelixHttpsKeystoreKeyTypeHint.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.keystore.key@TypeHint",
                          orgApacheFelixHttpsKeystoreKeyTypeHint.Value
                      )
                  if orgApacheFelixHttpsKeystoreKeyPassword.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.keystore.key.password",
                          orgApacheFelixHttpsKeystoreKeyPassword.Value
                      )
                  if orgApacheFelixHttpsKeystoreKeyPasswordTypeHint.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.keystore.key.password@TypeHint",
                          orgApacheFelixHttpsKeystoreKeyPasswordTypeHint.Value
                      )
                  if orgApacheFelixHttpsTruststore.IsSome then
                      RequestPart.query ("org.apache.felix.https.truststore", orgApacheFelixHttpsTruststore.Value)
                  if orgApacheFelixHttpsTruststoreTypeHint.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.truststore@TypeHint",
                          orgApacheFelixHttpsTruststoreTypeHint.Value
                      )
                  if orgApacheFelixHttpsTruststorePassword.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.truststore.password",
                          orgApacheFelixHttpsTruststorePassword.Value
                      )
                  if orgApacheFelixHttpsTruststorePasswordTypeHint.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.truststore.password@TypeHint",
                          orgApacheFelixHttpsTruststorePasswordTypeHint.Value
                      )
                  if orgApacheFelixHttpsClientcertificate.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.clientcertificate",
                          orgApacheFelixHttpsClientcertificate.Value
                      )
                  if orgApacheFelixHttpsClientcertificateTypeHint.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.clientcertificate@TypeHint",
                          orgApacheFelixHttpsClientcertificateTypeHint.Value
                      )
                  if orgApacheFelixHttpsEnable.IsSome then
                      RequestPart.query ("org.apache.felix.https.enable", orgApacheFelixHttpsEnable.Value)
                  if orgApacheFelixHttpsEnableTypeHint.IsSome then
                      RequestPart.query (
                          "org.apache.felix.https.enable@TypeHint",
                          orgApacheFelixHttpsEnableTypeHint.Value
                      )
                  if orgOsgiServiceHttpPortSecure.IsSome then
                      RequestPart.query ("org.osgi.service.http.port.secure", orgOsgiServiceHttpPortSecure.Value)
                  if orgOsgiServiceHttpPortSecureTypeHint.IsSome then
                      RequestPart.query (
                          "org.osgi.service.http.port.secure@TypeHint",
                          orgOsgiServiceHttpPortSecureTypeHint.Value
                      ) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/apps/system/config/org.apache.felix.http"
                    requestParts
                    cancellationToken

            return PostConfigApacheFelixJettyBasedHttpService.DefaultResponse
        }

    member this.PostConfigApacheHttpComponentsProxyConfiguration
        (
            ?proxyHost: string,
            ?proxyHostTypeHint: string,
            ?proxyPort: int,
            ?proxyPortTypeHint: string,
            ?proxyExceptions: list<string>,
            ?proxyExceptionsTypeHint: string,
            ?proxyEnabled: bool,
            ?proxyEnabledTypeHint: string,
            ?proxyUser: string,
            ?proxyUserTypeHint: string,
            ?proxyPassword: string,
            ?proxyPasswordTypeHint: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if proxyHost.IsSome then
                      RequestPart.query ("proxy.host", proxyHost.Value)
                  if proxyHostTypeHint.IsSome then
                      RequestPart.query ("proxy.host@TypeHint", proxyHostTypeHint.Value)
                  if proxyPort.IsSome then
                      RequestPart.query ("proxy.port", proxyPort.Value)
                  if proxyPortTypeHint.IsSome then
                      RequestPart.query ("proxy.port@TypeHint", proxyPortTypeHint.Value)
                  if proxyExceptions.IsSome then
                      RequestPart.query ("proxy.exceptions", proxyExceptions.Value)
                  if proxyExceptionsTypeHint.IsSome then
                      RequestPart.query ("proxy.exceptions@TypeHint", proxyExceptionsTypeHint.Value)
                  if proxyEnabled.IsSome then
                      RequestPart.query ("proxy.enabled", proxyEnabled.Value)
                  if proxyEnabledTypeHint.IsSome then
                      RequestPart.query ("proxy.enabled@TypeHint", proxyEnabledTypeHint.Value)
                  if proxyUser.IsSome then
                      RequestPart.query ("proxy.user", proxyUser.Value)
                  if proxyUserTypeHint.IsSome then
                      RequestPart.query ("proxy.user@TypeHint", proxyUserTypeHint.Value)
                  if proxyPassword.IsSome then
                      RequestPart.query ("proxy.password", proxyPassword.Value)
                  if proxyPasswordTypeHint.IsSome then
                      RequestPart.query ("proxy.password@TypeHint", proxyPasswordTypeHint.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/apps/system/config/org.apache.http.proxyconfigurator.config"
                    requestParts
                    cancellationToken

            return PostConfigApacheHttpComponentsProxyConfiguration.DefaultResponse
        }

    member this.PostConfigApacheSlingDavExServlet
        (
            ?alias: string,
            ?aliasTypeHint: string,
            ?davCreateAbsoluteUri: bool,
            ?davCreateAbsoluteUriTypeHint: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if alias.IsSome then
                      RequestPart.query ("alias", alias.Value)
                  if aliasTypeHint.IsSome then
                      RequestPart.query ("alias@TypeHint", aliasTypeHint.Value)
                  if davCreateAbsoluteUri.IsSome then
                      RequestPart.query ("dav.create-absolute-uri", davCreateAbsoluteUri.Value)
                  if davCreateAbsoluteUriTypeHint.IsSome then
                      RequestPart.query ("dav.create-absolute-uri@TypeHint", davCreateAbsoluteUriTypeHint.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/apps/system/config/org.apache.sling.jcr.davex.impl.servlets.SlingDavExServlet"
                    requestParts
                    cancellationToken

            return PostConfigApacheSlingDavExServlet.DefaultResponse
        }

    member this.PostConfigApacheSlingReferrerFilter
        (
            ?allowEmpty: bool,
            ?allowEmptyTypeHint: string,
            ?allowHosts: string,
            ?allowHostsTypeHint: string,
            ?allowHostsRegexp: string,
            ?allowHostsRegexpTypeHint: string,
            ?filterMethods: string,
            ?filterMethodsTypeHint: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if allowEmpty.IsSome then
                      RequestPart.query ("allow.empty", allowEmpty.Value)
                  if allowEmptyTypeHint.IsSome then
                      RequestPart.query ("allow.empty@TypeHint", allowEmptyTypeHint.Value)
                  if allowHosts.IsSome then
                      RequestPart.query ("allow.hosts", allowHosts.Value)
                  if allowHostsTypeHint.IsSome then
                      RequestPart.query ("allow.hosts@TypeHint", allowHostsTypeHint.Value)
                  if allowHostsRegexp.IsSome then
                      RequestPart.query ("allow.hosts.regexp", allowHostsRegexp.Value)
                  if allowHostsRegexpTypeHint.IsSome then
                      RequestPart.query ("allow.hosts.regexp@TypeHint", allowHostsRegexpTypeHint.Value)
                  if filterMethods.IsSome then
                      RequestPart.query ("filter.methods", filterMethods.Value)
                  if filterMethodsTypeHint.IsSome then
                      RequestPart.query ("filter.methods@TypeHint", filterMethodsTypeHint.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/apps/system/config/org.apache.sling.security.impl.ReferrerFilter"
                    requestParts
                    cancellationToken

            return PostConfigApacheSlingReferrerFilter.DefaultResponse
        }

    member this.PostConfigApacheSlingGetServlet
        (
            ?jsonMaximumresults: string,
            ?jsonMaximumresultsTypeHint: string,
            ?enableHtml: bool,
            ?enableHtmlTypeHint: string,
            ?enableTxt: bool,
            ?enableTxtTypeHint: string,
            ?enableXml: bool,
            ?enableXmlTypeHint: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if jsonMaximumresults.IsSome then
                      RequestPart.query ("json.maximumresults", jsonMaximumresults.Value)
                  if jsonMaximumresultsTypeHint.IsSome then
                      RequestPart.query ("json.maximumresults@TypeHint", jsonMaximumresultsTypeHint.Value)
                  if enableHtml.IsSome then
                      RequestPart.query ("enable.html", enableHtml.Value)
                  if enableHtmlTypeHint.IsSome then
                      RequestPart.query ("enable.html@TypeHint", enableHtmlTypeHint.Value)
                  if enableTxt.IsSome then
                      RequestPart.query ("enable.txt", enableTxt.Value)
                  if enableTxtTypeHint.IsSome then
                      RequestPart.query ("enable.txt@TypeHint", enableTxtTypeHint.Value)
                  if enableXml.IsSome then
                      RequestPart.query ("enable.xml", enableXml.Value)
                  if enableXmlTypeHint.IsSome then
                      RequestPart.query ("enable.xml@TypeHint", enableXmlTypeHint.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/apps/system/config/org.apache.sling.servlets.get.DefaultGetServlet"
                    requestParts
                    cancellationToken

            return PostConfigApacheSlingGetServlet.DefaultResponse
        }

    member this.PostConfigProperty(configNodeName: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("configNodeName", configNodeName) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/apps/system/config/{configNodeName}" requestParts cancellationToken

            return PostConfigProperty.DefaultResponse
        }

    member this.GetQuery
        (
            path: string,
            pLimit: float,
            property: string,
            propertyValue: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.query ("path", path)
                  RequestPart.query ("p.limit", pLimit)
                  RequestPart.query ("1_property", property)
                  RequestPart.query ("1_property.value", propertyValue) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/bin/querybuilder.json" requestParts cancellationToken

            return GetQuery.DefaultResponse content
        }

    member this.PostQuery
        (
            path: string,
            pLimit: float,
            property: string,
            propertyValue: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.query ("path", path)
                  RequestPart.query ("p.limit", pLimit)
                  RequestPart.query ("1_property", property)
                  RequestPart.query ("1_property.value", propertyValue) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/bin/querybuilder.json" requestParts cancellationToken

            return PostQuery.DefaultResponse content
        }

    member this.PostSetPassword(old: string, plain: string, verify: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.query ("old", old)
                  RequestPart.query ("plain", plain)
                  RequestPart.query ("verify", verify) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/crx/explorer/ui/setpassword.jsp" requestParts cancellationToken

            return PostSetPassword.DefaultResponse content
        }

    member this.GetInstallStatus(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/crx/packmgr/installstatus.jsp" requestParts cancellationToken

            match int status with
            | 200 -> return GetInstallStatus.OK(Serializer.deserialize content)
            | _ -> return GetInstallStatus.DefaultResponse content
        }

    member this.PostPackageService(cmd: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.query ("cmd", cmd) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/crx/packmgr/service.jsp" requestParts cancellationToken

            return PostPackageService.DefaultResponse
        }

    member this.PostPackageServiceJson
        (
            path: string,
            cmd: string,
            ?groupName: string,
            ?packageName: string,
            ?packageVersion: string,
            ?charset: string,
            ?force: bool,
            ?recursive: bool,
            ?cancellationToken: CancellationToken,
            ?package: string
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("path", path)
                  RequestPart.query ("cmd", cmd)
                  if groupName.IsSome then
                      RequestPart.query ("groupName", groupName.Value)
                  if packageName.IsSome then
                      RequestPart.query ("packageName", packageName.Value)
                  if packageVersion.IsSome then
                      RequestPart.query ("packageVersion", packageVersion.Value)
                  if charset.IsSome then
                      RequestPart.query ("_charset_", charset.Value)
                  if force.IsSome then
                      RequestPart.query ("force", force.Value)
                  if recursive.IsSome then
                      RequestPart.query ("recursive", recursive.Value)
                  if package.IsSome then
                      RequestPart.multipartFormData ("package", package.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/crx/packmgr/service/.json/{path}" requestParts cancellationToken

            return PostPackageServiceJson.DefaultResponse content
        }

    member this.GetPackageManagerServlet(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/crx/packmgr/service/script.html" requestParts cancellationToken

            match int status with
            | 404 -> return GetPackageManagerServlet.NotFound
            | 405 -> return GetPackageManagerServlet.MethodNotAllowed
            | _ -> return GetPackageManagerServlet.DefaultResponse
        }

    member this.PostPackageUpdate
        (
            groupName: string,
            packageName: string,
            version: string,
            path: string,
            ?filter: string,
            ?charset: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.query ("groupName", groupName)
                  RequestPart.query ("packageName", packageName)
                  RequestPart.query ("version", version)
                  RequestPart.query ("path", path)
                  if filter.IsSome then
                      RequestPart.query ("filter", filter.Value)
                  if charset.IsSome then
                      RequestPart.query ("_charset_", charset.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/crx/packmgr/update.jsp" requestParts cancellationToken

            return PostPackageUpdate.DefaultResponse content
        }

    member this.GetCrxdeStatus(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/crx/server/crx.default/jcr:root/.1.json"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetCrxdeStatus.OK
            | _ -> return GetCrxdeStatus.NotFound
        }

    member this.GetPackage(group: string, name: string, version: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("group", group)
                  RequestPart.path ("name", name)
                  RequestPart.path ("version", version) ]

            let! (status, contentBinary) =
                OpenApiHttp.getBinaryAsync
                    httpClient
                    "/etc/packages/{group}/{name}-{version}.zip"
                    requestParts
                    cancellationToken

            return GetPackage.DefaultResponse contentBinary
        }

    member this.GetPackageFilter(group: string, name: string, version: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("group", group)
                  RequestPart.path ("name", name)
                  RequestPart.path ("version", version) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/etc/packages/{group}/{name}-{version}.zip/jcr:content/vlt:definition/filter.tidy.2.json"
                    requestParts
                    cancellationToken

            return GetPackageFilter.DefaultResponse content
        }

    member this.GetAgents(runmode: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("runmode", runmode) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/etc/replication/agents.{runmode}.-1.json"
                    requestParts
                    cancellationToken

            return GetAgents.DefaultResponse content
        }

    member this.DeleteAgent(runmode: string, name: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("runmode", runmode)
                  RequestPart.path ("name", name) ]

            let! (status, content) =
                OpenApiHttp.deleteAsync
                    httpClient
                    "/etc/replication/agents.{runmode}/{name}"
                    requestParts
                    cancellationToken

            return DeleteAgent.DefaultResponse
        }

    member this.GetAgent(runmode: string, name: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("runmode", runmode)
                  RequestPart.path ("name", name) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/etc/replication/agents.{runmode}/{name}"
                    requestParts
                    cancellationToken

            return GetAgent.DefaultResponse
        }

    member this.PostAgent
        (
            runmode: string,
            name: string,
            ?``jcrcontent/cqdistribute``: bool,
            ?``jcrcontent/cqdistributeTypeHint``: string,
            ?``jcrcontent/cqname``: string,
            ?``jcrcontent/cqtemplate``: string,
            ?``jcrcontent/enabled``: bool,
            ?``jcrcontent/jcrdescription``: string,
            ?``jcrcontent/jcrlastModified``: string,
            ?``jcrcontent/jcrlastModifiedBy``: string,
            ?``jcrcontent/jcrmixinTypes``: string,
            ?``jcrcontent/jcrtitle``: string,
            ?``jcrcontent/logLevel``: string,
            ?``jcrcontent/noStatusUpdate``: bool,
            ?``jcrcontent/noVersioning``: bool,
            ?``jcrcontent/protocolConnectTimeout``: float,
            ?``jcrcontent/protocolHTTPConnectionClosed``: bool,
            ?``jcrcontent/protocolHTTPExpired``: string,
            ?``jcrcontent/protocolHTTPHeaders``: list<string>,
            ?``jcrcontent/protocolHTTPHeadersTypeHint``: string,
            ?``jcrcontent/protocolHTTPMethod``: string,
            ?``jcrcontent/protocolHTTPSRelaxed``: bool,
            ?``jcrcontent/protocolInterface``: string,
            ?``jcrcontent/protocolSocketTimeout``: float,
            ?``jcrcontent/protocolVersion``: string,
            ?``jcrcontent/proxyNTLMDomain``: string,
            ?``jcrcontent/proxyNTLMHost``: string,
            ?``jcrcontent/proxyHost``: string,
            ?``jcrcontent/proxyPassword``: string,
            ?``jcrcontent/proxyPort``: float,
            ?``jcrcontent/proxyUser``: string,
            ?``jcrcontent/queueBatchMaxSize``: float,
            ?``jcrcontent/queueBatchMode``: string,
            ?``jcrcontent/queueBatchWaitTime``: float,
            ?``jcrcontent/retryDelay``: string,
            ?``jcrcontent/reverseReplication``: bool,
            ?``jcrcontent/serializationType``: string,
            ?``jcrcontent/slingresourceType``: string,
            ?``jcrcontent/ssl``: string,
            ?``jcrcontent/transportNTLMDomain``: string,
            ?``jcrcontent/transportNTLMHost``: string,
            ?``jcrcontent/transportPassword``: string,
            ?``jcrcontent/transportUri``: string,
            ?``jcrcontent/transportUser``: string,
            ?``jcrcontent/triggerDistribute``: bool,
            ?``jcrcontent/triggerModified``: bool,
            ?``jcrcontent/triggerOnOffTime``: bool,
            ?``jcrcontent/triggerReceive``: bool,
            ?``jcrcontent/triggerSpecific``: bool,
            ?``jcrcontent/userId``: string,
            ?jcrprimaryType: string,
            ?operation: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("runmode", runmode)
                  RequestPart.path ("name", name)
                  if ``jcrcontent/cqdistribute``.IsSome then
                      RequestPart.query ("jcr:content/cq:distribute", ``jcrcontent/cqdistribute``.Value)
                  if ``jcrcontent/cqdistributeTypeHint``.IsSome then
                      RequestPart.query (
                          "jcr:content/cq:distribute@TypeHint",
                          ``jcrcontent/cqdistributeTypeHint``.Value
                      )
                  if ``jcrcontent/cqname``.IsSome then
                      RequestPart.query ("jcr:content/cq:name", ``jcrcontent/cqname``.Value)
                  if ``jcrcontent/cqtemplate``.IsSome then
                      RequestPart.query ("jcr:content/cq:template", ``jcrcontent/cqtemplate``.Value)
                  if ``jcrcontent/enabled``.IsSome then
                      RequestPart.query ("jcr:content/enabled", ``jcrcontent/enabled``.Value)
                  if ``jcrcontent/jcrdescription``.IsSome then
                      RequestPart.query ("jcr:content/jcr:description", ``jcrcontent/jcrdescription``.Value)
                  if ``jcrcontent/jcrlastModified``.IsSome then
                      RequestPart.query ("jcr:content/jcr:lastModified", ``jcrcontent/jcrlastModified``.Value)
                  if ``jcrcontent/jcrlastModifiedBy``.IsSome then
                      RequestPart.query ("jcr:content/jcr:lastModifiedBy", ``jcrcontent/jcrlastModifiedBy``.Value)
                  if ``jcrcontent/jcrmixinTypes``.IsSome then
                      RequestPart.query ("jcr:content/jcr:mixinTypes", ``jcrcontent/jcrmixinTypes``.Value)
                  if ``jcrcontent/jcrtitle``.IsSome then
                      RequestPart.query ("jcr:content/jcr:title", ``jcrcontent/jcrtitle``.Value)
                  if ``jcrcontent/logLevel``.IsSome then
                      RequestPart.query ("jcr:content/logLevel", ``jcrcontent/logLevel``.Value)
                  if ``jcrcontent/noStatusUpdate``.IsSome then
                      RequestPart.query ("jcr:content/noStatusUpdate", ``jcrcontent/noStatusUpdate``.Value)
                  if ``jcrcontent/noVersioning``.IsSome then
                      RequestPart.query ("jcr:content/noVersioning", ``jcrcontent/noVersioning``.Value)
                  if ``jcrcontent/protocolConnectTimeout``.IsSome then
                      RequestPart.query (
                          "jcr:content/protocolConnectTimeout",
                          ``jcrcontent/protocolConnectTimeout``.Value
                      )
                  if ``jcrcontent/protocolHTTPConnectionClosed``.IsSome then
                      RequestPart.query (
                          "jcr:content/protocolHTTPConnectionClosed",
                          ``jcrcontent/protocolHTTPConnectionClosed``.Value
                      )
                  if ``jcrcontent/protocolHTTPExpired``.IsSome then
                      RequestPart.query ("jcr:content/protocolHTTPExpired", ``jcrcontent/protocolHTTPExpired``.Value)
                  if ``jcrcontent/protocolHTTPHeaders``.IsSome then
                      RequestPart.query ("jcr:content/protocolHTTPHeaders", ``jcrcontent/protocolHTTPHeaders``.Value)
                  if ``jcrcontent/protocolHTTPHeadersTypeHint``.IsSome then
                      RequestPart.query (
                          "jcr:content/protocolHTTPHeaders@TypeHint",
                          ``jcrcontent/protocolHTTPHeadersTypeHint``.Value
                      )
                  if ``jcrcontent/protocolHTTPMethod``.IsSome then
                      RequestPart.query ("jcr:content/protocolHTTPMethod", ``jcrcontent/protocolHTTPMethod``.Value)
                  if ``jcrcontent/protocolHTTPSRelaxed``.IsSome then
                      RequestPart.query ("jcr:content/protocolHTTPSRelaxed", ``jcrcontent/protocolHTTPSRelaxed``.Value)
                  if ``jcrcontent/protocolInterface``.IsSome then
                      RequestPart.query ("jcr:content/protocolInterface", ``jcrcontent/protocolInterface``.Value)
                  if ``jcrcontent/protocolSocketTimeout``.IsSome then
                      RequestPart.query (
                          "jcr:content/protocolSocketTimeout",
                          ``jcrcontent/protocolSocketTimeout``.Value
                      )
                  if ``jcrcontent/protocolVersion``.IsSome then
                      RequestPart.query ("jcr:content/protocolVersion", ``jcrcontent/protocolVersion``.Value)
                  if ``jcrcontent/proxyNTLMDomain``.IsSome then
                      RequestPart.query ("jcr:content/proxyNTLMDomain", ``jcrcontent/proxyNTLMDomain``.Value)
                  if ``jcrcontent/proxyNTLMHost``.IsSome then
                      RequestPart.query ("jcr:content/proxyNTLMHost", ``jcrcontent/proxyNTLMHost``.Value)
                  if ``jcrcontent/proxyHost``.IsSome then
                      RequestPart.query ("jcr:content/proxyHost", ``jcrcontent/proxyHost``.Value)
                  if ``jcrcontent/proxyPassword``.IsSome then
                      RequestPart.query ("jcr:content/proxyPassword", ``jcrcontent/proxyPassword``.Value)
                  if ``jcrcontent/proxyPort``.IsSome then
                      RequestPart.query ("jcr:content/proxyPort", ``jcrcontent/proxyPort``.Value)
                  if ``jcrcontent/proxyUser``.IsSome then
                      RequestPart.query ("jcr:content/proxyUser", ``jcrcontent/proxyUser``.Value)
                  if ``jcrcontent/queueBatchMaxSize``.IsSome then
                      RequestPart.query ("jcr:content/queueBatchMaxSize", ``jcrcontent/queueBatchMaxSize``.Value)
                  if ``jcrcontent/queueBatchMode``.IsSome then
                      RequestPart.query ("jcr:content/queueBatchMode", ``jcrcontent/queueBatchMode``.Value)
                  if ``jcrcontent/queueBatchWaitTime``.IsSome then
                      RequestPart.query ("jcr:content/queueBatchWaitTime", ``jcrcontent/queueBatchWaitTime``.Value)
                  if ``jcrcontent/retryDelay``.IsSome then
                      RequestPart.query ("jcr:content/retryDelay", ``jcrcontent/retryDelay``.Value)
                  if ``jcrcontent/reverseReplication``.IsSome then
                      RequestPart.query ("jcr:content/reverseReplication", ``jcrcontent/reverseReplication``.Value)
                  if ``jcrcontent/serializationType``.IsSome then
                      RequestPart.query ("jcr:content/serializationType", ``jcrcontent/serializationType``.Value)
                  if ``jcrcontent/slingresourceType``.IsSome then
                      RequestPart.query ("jcr:content/sling:resourceType", ``jcrcontent/slingresourceType``.Value)
                  if ``jcrcontent/ssl``.IsSome then
                      RequestPart.query ("jcr:content/ssl", ``jcrcontent/ssl``.Value)
                  if ``jcrcontent/transportNTLMDomain``.IsSome then
                      RequestPart.query ("jcr:content/transportNTLMDomain", ``jcrcontent/transportNTLMDomain``.Value)
                  if ``jcrcontent/transportNTLMHost``.IsSome then
                      RequestPart.query ("jcr:content/transportNTLMHost", ``jcrcontent/transportNTLMHost``.Value)
                  if ``jcrcontent/transportPassword``.IsSome then
                      RequestPart.query ("jcr:content/transportPassword", ``jcrcontent/transportPassword``.Value)
                  if ``jcrcontent/transportUri``.IsSome then
                      RequestPart.query ("jcr:content/transportUri", ``jcrcontent/transportUri``.Value)
                  if ``jcrcontent/transportUser``.IsSome then
                      RequestPart.query ("jcr:content/transportUser", ``jcrcontent/transportUser``.Value)
                  if ``jcrcontent/triggerDistribute``.IsSome then
                      RequestPart.query ("jcr:content/triggerDistribute", ``jcrcontent/triggerDistribute``.Value)
                  if ``jcrcontent/triggerModified``.IsSome then
                      RequestPart.query ("jcr:content/triggerModified", ``jcrcontent/triggerModified``.Value)
                  if ``jcrcontent/triggerOnOffTime``.IsSome then
                      RequestPart.query ("jcr:content/triggerOnOffTime", ``jcrcontent/triggerOnOffTime``.Value)
                  if ``jcrcontent/triggerReceive``.IsSome then
                      RequestPart.query ("jcr:content/triggerReceive", ``jcrcontent/triggerReceive``.Value)
                  if ``jcrcontent/triggerSpecific``.IsSome then
                      RequestPart.query ("jcr:content/triggerSpecific", ``jcrcontent/triggerSpecific``.Value)
                  if ``jcrcontent/userId``.IsSome then
                      RequestPart.query ("jcr:content/userId", ``jcrcontent/userId``.Value)
                  if jcrprimaryType.IsSome then
                      RequestPart.query ("jcr:primaryType", jcrprimaryType.Value)
                  if operation.IsSome then
                      RequestPart.query (":operation", operation.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/etc/replication/agents.{runmode}/{name}"
                    requestParts
                    cancellationToken

            return PostAgent.DefaultResponse
        }

    member this.PostTruststorePKCS12(?cancellationToken: CancellationToken, ?truststoreP12: string) =
        async {
            let requestParts =
                [ if truststoreP12.IsSome then
                      RequestPart.multipartFormData ("truststore.p12", truststoreP12.Value) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/etc/truststore" requestParts cancellationToken
            return PostTruststorePKCS12.DefaultResponse content
        }

    member this.GetTruststore(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, contentBinary) =
                OpenApiHttp.getBinaryAsync httpClient "/etc/truststore/truststore.p12" requestParts cancellationToken

            return GetTruststore.DefaultResponse contentBinary
        }

    member this.GetLoginPage(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/libs/granite/core/content/login.html" requestParts cancellationToken

            return GetLoginPage.DefaultResponse
        }

    member this.PostAuthorizables
        (
            authorizableId: string,
            intermediatePath: string,
            ?createUser: string,
            ?createGroup: string,
            ?reppassword: string,
            ?``profile/givenName``: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.query ("authorizableId", authorizableId)
                  RequestPart.query ("intermediatePath", intermediatePath)
                  if createUser.IsSome then
                      RequestPart.query ("createUser", createUser.Value)
                  if createGroup.IsSome then
                      RequestPart.query ("createGroup", createGroup.Value)
                  if reppassword.IsSome then
                      RequestPart.query ("rep:password", reppassword.Value)
                  if ``profile/givenName``.IsSome then
                      RequestPart.query ("profile/givenName", ``profile/givenName``.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/libs/granite/security/post/authorizables"
                    requestParts
                    cancellationToken

            return PostAuthorizables.DefaultResponse
        }

    member this.SslSetup
        (
            keystorePassword: string,
            keystorePasswordConfirm: string,
            truststorePassword: string,
            truststorePasswordConfirm: string,
            httpsHostname: string,
            httpsPort: string,
            ?cancellationToken: CancellationToken,
            ?privatekeyFile: string,
            ?certificateFile: string
        ) =
        async {
            let requestParts =
                [ RequestPart.query ("keystorePassword", keystorePassword)
                  RequestPart.query ("keystorePasswordConfirm", keystorePasswordConfirm)
                  RequestPart.query ("truststorePassword", truststorePassword)
                  RequestPart.query ("truststorePasswordConfirm", truststorePasswordConfirm)
                  RequestPart.query ("httpsHostname", httpsHostname)
                  RequestPart.query ("httpsPort", httpsPort)
                  if privatekeyFile.IsSome then
                      RequestPart.multipartFormData ("privatekeyFile", privatekeyFile.Value)
                  if certificateFile.IsSome then
                      RequestPart.multipartFormData ("certificateFile", certificateFile.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/libs/granite/security/post/sslSetup.html"
                    requestParts
                    cancellationToken

            return SslSetup.DefaultResponse content
        }

    member this.PostTruststore
        (
            ?operation: string,
            ?newPassword: string,
            ?rePassword: string,
            ?keyStoreType: string,
            ?removeAlias: string,
            ?cancellationToken: CancellationToken,
            ?certificate: string
        ) =
        async {
            let requestParts =
                [ if operation.IsSome then
                      RequestPart.query (":operation", operation.Value)
                  if newPassword.IsSome then
                      RequestPart.query ("newPassword", newPassword.Value)
                  if rePassword.IsSome then
                      RequestPart.query ("rePassword", rePassword.Value)
                  if keyStoreType.IsSome then
                      RequestPart.query ("keyStoreType", keyStoreType.Value)
                  if removeAlias.IsSome then
                      RequestPart.query ("removeAlias", removeAlias.Value)
                  if certificate.IsSome then
                      RequestPart.multipartFormData ("certificate", certificate.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/libs/granite/security/post/truststore" requestParts cancellationToken

            return PostTruststore.DefaultResponse content
        }

    member this.GetTruststoreInfo(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/libs/granite/security/truststore.json" requestParts cancellationToken

            match int status with
            | 200 -> return GetTruststoreInfo.OK(Serializer.deserialize content)
            | _ -> return GetTruststoreInfo.DefaultResponse content
        }

    member this.PostTreeActivation
        (
            ignoredeactivated: bool,
            onlymodified: bool,
            path: string,
            cmd: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.query ("ignoredeactivated", ignoredeactivated)
                  RequestPart.query ("onlymodified", onlymodified)
                  RequestPart.query ("path", path)
                  RequestPart.query ("cmd", cmd) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/libs/replication/treeactivation.html" requestParts cancellationToken

            return PostTreeActivation.DefaultResponse
        }

    member this.PostBundle(name: string, action: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("name", name)
                  RequestPart.query ("action", action) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/system/console/bundles/{name}" requestParts cancellationToken

            return PostBundle.DefaultResponse
        }

    member this.GetBundleInfo(name: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("name", name) ]

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/system/console/bundles/{name}.json" requestParts cancellationToken

            match int status with
            | 200 -> return GetBundleInfo.OK(Serializer.deserialize content)
            | _ -> return GetBundleInfo.DefaultResponse content
        }

    member this.GetConfigMgr(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/system/console/configMgr" requestParts cancellationToken

            return GetConfigMgr.OK
        }

    member this.PostSamlConfiguration
        (
            ?post: bool,
            ?apply: bool,
            ?delete: bool,
            ?action: string,
            ?location: string,
            ?path: list<string>,
            ?serviceRanking: int,
            ?idpUrl: string,
            ?idpCertAlias: string,
            ?idpHttpRedirect: bool,
            ?serviceProviderEntityId: string,
            ?assertionConsumerServiceURL: string,
            ?spPrivateKeyAlias: string,
            ?keyStorePassword: string,
            ?defaultRedirectUrl: string,
            ?userIDAttribute: string,
            ?useEncryption: bool,
            ?createUser: bool,
            ?addGroupMemberships: bool,
            ?groupMembershipAttribute: string,
            ?defaultGroups: list<string>,
            ?nameIdFormat: string,
            ?synchronizeAttributes: list<string>,
            ?handleLogout: bool,
            ?logoutUrl: string,
            ?clockTolerance: int,
            ?digestMethod: string,
            ?signatureMethod: string,
            ?userIntermediatePath: string,
            ?propertylist: list<string>,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ if post.IsSome then
                      RequestPart.query ("post", post.Value)
                  if apply.IsSome then
                      RequestPart.query ("apply", apply.Value)
                  if delete.IsSome then
                      RequestPart.query ("delete", delete.Value)
                  if action.IsSome then
                      RequestPart.query ("action", action.Value)
                  if location.IsSome then
                      RequestPart.query ("$location", location.Value)
                  if path.IsSome then
                      RequestPart.query ("path", path.Value)
                  if serviceRanking.IsSome then
                      RequestPart.query ("service.ranking", serviceRanking.Value)
                  if idpUrl.IsSome then
                      RequestPart.query ("idpUrl", idpUrl.Value)
                  if idpCertAlias.IsSome then
                      RequestPart.query ("idpCertAlias", idpCertAlias.Value)
                  if idpHttpRedirect.IsSome then
                      RequestPart.query ("idpHttpRedirect", idpHttpRedirect.Value)
                  if serviceProviderEntityId.IsSome then
                      RequestPart.query ("serviceProviderEntityId", serviceProviderEntityId.Value)
                  if assertionConsumerServiceURL.IsSome then
                      RequestPart.query ("assertionConsumerServiceURL", assertionConsumerServiceURL.Value)
                  if spPrivateKeyAlias.IsSome then
                      RequestPart.query ("spPrivateKeyAlias", spPrivateKeyAlias.Value)
                  if keyStorePassword.IsSome then
                      RequestPart.query ("keyStorePassword", keyStorePassword.Value)
                  if defaultRedirectUrl.IsSome then
                      RequestPart.query ("defaultRedirectUrl", defaultRedirectUrl.Value)
                  if userIDAttribute.IsSome then
                      RequestPart.query ("userIDAttribute", userIDAttribute.Value)
                  if useEncryption.IsSome then
                      RequestPart.query ("useEncryption", useEncryption.Value)
                  if createUser.IsSome then
                      RequestPart.query ("createUser", createUser.Value)
                  if addGroupMemberships.IsSome then
                      RequestPart.query ("addGroupMemberships", addGroupMemberships.Value)
                  if groupMembershipAttribute.IsSome then
                      RequestPart.query ("groupMembershipAttribute", groupMembershipAttribute.Value)
                  if defaultGroups.IsSome then
                      RequestPart.query ("defaultGroups", defaultGroups.Value)
                  if nameIdFormat.IsSome then
                      RequestPart.query ("nameIdFormat", nameIdFormat.Value)
                  if synchronizeAttributes.IsSome then
                      RequestPart.query ("synchronizeAttributes", synchronizeAttributes.Value)
                  if handleLogout.IsSome then
                      RequestPart.query ("handleLogout", handleLogout.Value)
                  if logoutUrl.IsSome then
                      RequestPart.query ("logoutUrl", logoutUrl.Value)
                  if clockTolerance.IsSome then
                      RequestPart.query ("clockTolerance", clockTolerance.Value)
                  if digestMethod.IsSome then
                      RequestPart.query ("digestMethod", digestMethod.Value)
                  if signatureMethod.IsSome then
                      RequestPart.query ("signatureMethod", signatureMethod.Value)
                  if userIntermediatePath.IsSome then
                      RequestPart.query ("userIntermediatePath", userIntermediatePath.Value)
                  if propertylist.IsSome then
                      RequestPart.query ("propertylist", propertylist.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/system/console/configMgr/com.adobe.granite.auth.saml.SamlAuthenticationHandler"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return PostSamlConfiguration.OK content
            | 302 -> return PostSamlConfiguration.Found content
            | _ -> return PostSamlConfiguration.DefaultResponse content
        }

    member this.PostJmxRepository(action: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts = [ RequestPart.path ("action", action) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/system/console/jmx/com.adobe.granite:type=Repository/op/{action}"
                    requestParts
                    cancellationToken

            return PostJmxRepository.DefaultResponse
        }

    member this.GetAemProductInfo(?cancellationToken: CancellationToken) =
        async {
            let requestParts = []

            let! (status, content) =
                OpenApiHttp.getAsync httpClient "/system/console/status-productinfo.json" requestParts cancellationToken

            return GetAemProductInfo.DefaultResponse(Serializer.deserialize content)
        }

    member this.GetAemHealthCheck(?tags: string, ?combineTagsOr: bool, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ if tags.IsSome then
                      RequestPart.query ("tags", tags.Value)
                  if combineTagsOr.IsSome then
                      RequestPart.query ("combineTagsOr", combineTagsOr.Value) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/system/health" requestParts cancellationToken
            return GetAemHealthCheck.DefaultResponse content
        }

    member this.PostAuthorizableKeystore
        (
            intermediatePath: string,
            authorizableId: string,
            ?operation: string,
            ?currentPassword: string,
            ?newPassword: string,
            ?rePassword: string,
            ?keyPassword: string,
            ?keyStorePass: string,
            ?alias: string,
            ?newAlias: string,
            ?removeAlias: string,
            ?cancellationToken: CancellationToken,
            ?pk: string,
            ?keyStore: string,
            ?certChain: string
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("intermediatePath", intermediatePath)
                  RequestPart.path ("authorizableId", authorizableId)
                  if operation.IsSome then
                      RequestPart.query (":operation", operation.Value)
                  if currentPassword.IsSome then
                      RequestPart.query ("currentPassword", currentPassword.Value)
                  if newPassword.IsSome then
                      RequestPart.query ("newPassword", newPassword.Value)
                  if rePassword.IsSome then
                      RequestPart.query ("rePassword", rePassword.Value)
                  if keyPassword.IsSome then
                      RequestPart.query ("keyPassword", keyPassword.Value)
                  if keyStorePass.IsSome then
                      RequestPart.query ("keyStorePass", keyStorePass.Value)
                  if alias.IsSome then
                      RequestPart.query ("alias", alias.Value)
                  if newAlias.IsSome then
                      RequestPart.query ("newAlias", newAlias.Value)
                  if removeAlias.IsSome then
                      RequestPart.query ("removeAlias", removeAlias.Value)
                  if pk.IsSome then
                      RequestPart.multipartFormData ("pk", pk.Value)
                  if keyStore.IsSome then
                      RequestPart.multipartFormData ("keyStore", keyStore.Value)
                  if certChain.IsSome then
                      RequestPart.multipartFormData ("cert-chain", certChain.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync
                    httpClient
                    "/{intermediatePath}/{authorizableId}.ks.html"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return PostAuthorizableKeystore.OK content
            | _ -> return PostAuthorizableKeystore.DefaultResponse content
        }

    member this.GetAuthorizableKeystore
        (
            intermediatePath: string,
            authorizableId: string,
            ?cancellationToken: CancellationToken
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("intermediatePath", intermediatePath)
                  RequestPart.path ("authorizableId", authorizableId) ]

            let! (status, content) =
                OpenApiHttp.getAsync
                    httpClient
                    "/{intermediatePath}/{authorizableId}.ks.json"
                    requestParts
                    cancellationToken

            match int status with
            | 200 -> return GetAuthorizableKeystore.OK content
            | _ -> return GetAuthorizableKeystore.DefaultResponse content
        }

    member this.GetKeystore(intermediatePath: string, authorizableId: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("intermediatePath", intermediatePath)
                  RequestPart.path ("authorizableId", authorizableId) ]

            let! (status, contentBinary) =
                OpenApiHttp.getBinaryAsync
                    httpClient
                    "/{intermediatePath}/{authorizableId}/keystore/store.p12"
                    requestParts
                    cancellationToken

            return GetKeystore.DefaultResponse contentBinary
        }

    member this.PostPath(path: string, jcrprimaryType: string, name: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("path", path)
                  RequestPart.query ("jcr:primaryType", jcrprimaryType)
                  RequestPart.query (":name", name) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/{path}/" requestParts cancellationToken
            return PostPath.DefaultResponse
        }

    member this.DeleteNode(path: string, name: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("path", path)
                  RequestPart.path ("name", name) ]

            let! (status, content) = OpenApiHttp.deleteAsync httpClient "/{path}/{name}" requestParts cancellationToken
            return DeleteNode.DefaultResponse
        }

    member this.GetNode(path: string, name: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("path", path)
                  RequestPart.path ("name", name) ]

            let! (status, content) = OpenApiHttp.getAsync httpClient "/{path}/{name}" requestParts cancellationToken
            return GetNode.DefaultResponse
        }

    member this.PostNode
        (
            path: string,
            name: string,
            ?operation: string,
            ?deleteAuthorizable: string,
            ?cancellationToken: CancellationToken,
            ?file: string
        ) =
        async {
            let requestParts =
                [ RequestPart.path ("path", path)
                  RequestPart.path ("name", name)
                  if operation.IsSome then
                      RequestPart.query (":operation", operation.Value)
                  if deleteAuthorizable.IsSome then
                      RequestPart.query ("deleteAuthorizable", deleteAuthorizable.Value)
                  if file.IsSome then
                      RequestPart.multipartFormData ("file", file.Value) ]

            let! (status, content) = OpenApiHttp.postAsync httpClient "/{path}/{name}" requestParts cancellationToken
            return PostNode.DefaultResponse
        }

    member this.PostNodeRw(path: string, name: string, ?addMembers: string, ?cancellationToken: CancellationToken) =
        async {
            let requestParts =
                [ RequestPart.path ("path", path)
                  RequestPart.path ("name", name)
                  if addMembers.IsSome then
                      RequestPart.query ("addMembers", addMembers.Value) ]

            let! (status, content) =
                OpenApiHttp.postAsync httpClient "/{path}/{name}.rw.html" requestParts cancellationToken

            return PostNodeRw.DefaultResponse
        }
