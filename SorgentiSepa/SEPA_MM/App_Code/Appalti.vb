Imports Microsoft.VisualBasic


Public Class Appalti


    Private _id As Integer

    Private _num_repertorio As String
    Private _data_repertorio As String
    Private _id_tipo As Integer
    Private _tipo As String
    Private _sal As String
    Private _descrizione As String
    Private _anno_inizio As String
    Private _durata As String
    Private _asta_canone As String
    Private _asta_consumo As String
    Private _oneri_canone As String
    Private _oneri_consumo As String
    Private _penali As String
    Private _oneri As String
    Private _rifinizio As String
    Private _rifine As String
    Private _ivacanone As Integer
    Private _ivaconsumo As Integer
    Private _costo As String
    Private _id_lotto As Integer
    Private _descrizione_lotto As String
    Private _servizio As String
    Private _sconto_canone As String
    Private _sconto_consumo As String

    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            Me._id = value
        End Set
    End Property

    Public Property NUM_REPERTORIO() As String
        Get
            Return _num_repertorio
        End Get
        Set(ByVal value As String)
            Me._num_repertorio = value
        End Set
    End Property
    Public Property DATA_REPERTORIO() As String
        Get
            Return _data_repertorio
        End Get
        Set(ByVal value As String)
            Me._data_repertorio = value
        End Set
    End Property
    Public Property ID_TIPO() As Integer
        Get
            Return _id_tipo
        End Get
        Set(ByVal value As Integer)
            Me._id_tipo = value
        End Set
    End Property
    Public Property TIPO() As String
        Get
            Return _tipo
        End Get
        Set(ByVal value As String)
            Me._tipo = value
        End Set
    End Property
    Public Property SAL() As String
        Get
            Return _sal
        End Get
        Set(ByVal value As String)
            Me._sal = value
        End Set
    End Property

    Public Property DESCRIZIONE() As String
        Get
            Return _descrizione
        End Get
        Set(ByVal value As String)
            Me._descrizione = value
        End Set
    End Property
    Public Property ANNO_INIZIO() As String
        Get
            Return _anno_inizio
        End Get
        Set(ByVal value As String)
            Me._anno_inizio = value
        End Set
    End Property
    Public Property DURATA() As String
        Get
            Return _durata
        End Get
        Set(ByVal value As String)
            Me._durata = value
        End Set
    End Property

    Public Property ASTA_CANONE() As String
        Get
            Return _asta_canone
        End Get
        Set(ByVal value As String)
            Me._asta_canone = value
        End Set
    End Property
    Public Property ASTA_CONSUMO() As String
        Get
            Return _asta_consumo
        End Get
        Set(ByVal value As String)
            Me._asta_consumo = value
        End Set
    End Property
    Public Property ONERI_CANONE() As String
        Get
            Return _oneri_canone
        End Get
        Set(ByVal value As String)
            Me._oneri_canone = value
        End Set
    End Property
    Public Property ONERI_CONSUMO() As String
        Get
            Return _oneri_consumo
        End Get
        Set(ByVal value As String)
            Me._oneri_consumo = value
        End Set
    End Property

    Public Property ONERI() As String
        Get
            Return _oneri
        End Get
        Set(ByVal value As String)
            Me._oneri = value
        End Set
    End Property

    Public Property PENALI() As String
        Get
            Return _penali
        End Get
        Set(ByVal value As String)
            Me._penali = value
        End Set
    End Property

    Public Property RIFINIZIO() As String
        Get
            Return _rifinizio
        End Get
        Set(ByVal value As String)
            Me._rifinizio = value
        End Set
    End Property

    Public Property RIFINE() As String
        Get
            Return _rifine
        End Get
        Set(ByVal value As String)
            Me._rifine = value
        End Set
    End Property
    Public Property IVA_CANONE() As Integer
        Get
            Return _ivacanone
        End Get
        Set(ByVal value As Integer)
            Me._ivacanone = value
        End Set
    End Property
    Public Property IVA_CONSUMO() As Integer
        Get
            Return _ivaconsumo
        End Get
        Set(ByVal value As Integer)
            Me._ivaconsumo = value
        End Set
    End Property
    Public Property COSTO() As String
        Get
            Return _costo
        End Get
        Set(ByVal value As String)
            Me._costo = value
        End Set
    End Property
    Public Property ID_LOTTO() As Integer
        Get
            Return _id_lotto
        End Get
        Set(ByVal value As Integer)
            Me._id_lotto = value
        End Set
    End Property
    Public Property DESCRIZIONE_LOTTO() As String
        Get
            Return _descrizione_lotto
        End Get
        Set(ByVal value As String)
            Me._descrizione_lotto = value
        End Set
    End Property
    Public Property SERVIZIO() As String
        Get
            Return _servizio
        End Get
        Set(ByVal value As String)
            Me._servizio = value
        End Set
    End Property
    Public Property SCONTO_CANONE() As String
        Get
            Return _sconto_canone
        End Get
        Set(ByVal value As String)
            Me._sconto_canone = value
        End Set
    End Property
    Public Property SCONTO_CONSUMO() As String
        Get
            Return _sconto_consumo
        End Get
        Set(ByVal value As String)
            Me._sconto_consumo = value
        End Set
    End Property

    Public Sub New(ByVal id As Integer, ByVal num_repertorio As String, ByVal data_repertorio As String, ByVal id_tipo As Integer, ByVal tipo As String, ByVal sal As String, ByVal descrizione As String, ByVal anno_inizio As String, ByVal durata As String, ByVal asta_canone As String, ByVal asta_consumo As String, ByVal oneri_canone As String, ByVal oneri_consumo As String, ByVal oneri As String, ByVal penali As String, ByVal rifinizio As String, ByVal rifine As String, ByVal iva_canone As Integer, ByVal iva_consumo As Integer, ByVal costo As String, ByVal id_lotto As Integer, ByVal descrizione_lotto As String, ByVal servizio As String, ByVal sconto_canone As String, ByVal sconto_consumo As String)

        Me.ID = id
        Me.NUM_REPERTORIO = num_repertorio
        Me.DATA_REPERTORIO = data_repertorio
        Me.ID_TIPO = id_tipo
        Me.TIPO = tipo
        Me.SAL = sal
        Me.DESCRIZIONE = descrizione
        Me.ANNO_INIZIO = anno_inizio
        Me.DURATA = durata
        Me.ASTA_CANONE = asta_canone
        Me.ASTA_CONSUMO = asta_consumo
        Me.ONERI_CANONE = oneri_canone
        Me.ONERI_CONSUMO = oneri_consumo
        Me.ONERI = oneri
        Me.PENALI = penali
        Me.RIFINIZIO = rifinizio
        Me.RIFINE = rifine
        Me.IVA_CANONE = iva_canone
        Me.IVA_CONSUMO = iva_consumo
        Me.COSTO = costo
        Me.ID_LOTTO = id_lotto
        Me.DESCRIZIONE_LOTTO = descrizione_lotto
        Me.SERVIZIO = servizio
        Me.SCONTO_CANONE = sconto_canone
        Me.SCONTO_CONSUMO = sconto_canone
    End Sub

End Class

