Imports System.ServiceModel

' NOTA: è possibile utilizzare il comando "Rinomina" del menu di scelta rapida per modificare il nome di interfaccia "ISepacomLock" nel codice e nel file di configurazione contemporaneamente.
<ServiceContract()>
Public Interface ISepacomLock

    <OperationContract()>
    Sub DoWork()

End Interface
