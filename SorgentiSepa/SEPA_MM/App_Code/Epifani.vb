Imports Microsoft.VisualBasic


Public Class Epifani


    '*** INIZIO  EPIFANI
    '*********************************************
    '*** IMP_TERMICO GENERATORI [GENERATORI_TERMICI]
    Public Class Generatori
        Private _id As Integer

        Private _Modello As String
        Private _Matricola As String
        Private _Note As String
        Private _Anno As String
        Private _Potenza As Double
        Private _Fluido As String
        Private _Marc As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property MODELLO() As String
            Get
                Return _Modello
            End Get
            Set(ByVal value As String)
                Me._Modello = value
            End Set
        End Property

        Public Property MATRICOLA() As String
            Get
                Return _Matricola
            End Get
            Set(ByVal value As String)
                Me._Matricola = value
            End Set
        End Property


        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property

        Public Property ANNO_COSTRUZIONE() As String
            Get
                Return _Anno
            End Get
            Set(ByVal value As String)
                Me._Anno = value
            End Set
        End Property

        Public Property POTENZA() As Double
            Get
                Return _Potenza
            End Get
            Set(ByVal value As Double)
                Me._Potenza = value
            End Set
        End Property

        Public Property FLUIDO_TERMOVETTORE() As String
            Get
                Return _Fluido
            End Get
            Set(ByVal value As String)
                Me._Fluido = value
            End Set
        End Property

        Public Property MARC_EFF_ENERGETICA() As String
            Get
                Return _Marc
            End Get
            Set(ByVal value As String)
                Me._Marc = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal modello As String, ByVal matricola As String, ByVal note As String, ByVal anno As String, ByVal potenza As Double, ByVal fluido As String, ByVal marc As String)

            Me.ID = id
            Me.MODELLO = modello
            Me.MATRICOLA = matricola
            Me.NOTE = note
            Me.ANNO_COSTRUZIONE = anno
            Me.POTENZA = potenza
            Me.FLUIDO_TERMOVETTORE = fluido
            Me.MARC_EFF_ENERGETICA = marc

        End Sub

    End Class

    '*** IMP_TERMICO BRUCIATORI [BRUCIATORI]
    Public Class Bruciatori
        Private _id As Integer

        Private _Modello As String
        Private _Matricola As String
        Private _Note As String
        Private _Anno As String
        Private _CampoFunzionamento As Double
        Private _CampoFunzionamentoMax As Double


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property MODELLO() As String
            Get
                Return _Modello
            End Get
            Set(ByVal value As String)
                Me._Modello = value
            End Set
        End Property

        Public Property MATRICOLA() As String
            Get
                Return _Matricola
            End Get
            Set(ByVal value As String)
                Me._Matricola = value
            End Set
        End Property


        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property

        Public Property ANNO_COSTRUZIONE() As String
            Get
                Return _Anno
            End Get
            Set(ByVal value As String)
                Me._Anno = value
            End Set
        End Property

        Public Property CAMPO_FUNZIONAMENTO() As Double
            Get
                Return _CampoFunzionamento
            End Get
            Set(ByVal value As Double)
                Me._CampoFunzionamento = value
            End Set
        End Property

        Public Property CAMPO_FUNZIONAMENTO_MAX() As Double
            Get
                Return _CampoFunzionamentoMax
            End Get
            Set(ByVal value As Double)
                Me._CampoFunzionamentoMax = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal modello As String, ByVal matricola As String, ByVal note As String, ByVal anno As String, ByVal CampoFunzionamento As Double, ByVal CampoFunzionamentoMax As Double)

            Me.ID = id
            Me.MODELLO = modello
            Me.MATRICOLA = matricola
            Me.NOTE = note
            Me.ANNO_COSTRUZIONE = anno
            Me.CAMPO_FUNZIONAMENTO = CampoFunzionamento
            Me.CAMPO_FUNZIONAMENTO_MAX = CampoFunzionamentoMax

        End Sub

    End Class


    '*** IMP_TERMICO POMPE [POMPE_CIRCOLAZIONE_TERMICI]
    Public Class Pompe
        Private _id As Integer

        Private _Modello As String
        Private _Matricola As String
        Private _Note As String
        Private _Anno As String
        Private _Potenza As Double



        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property MODELLO() As String
            Get
                Return _Modello
            End Get
            Set(ByVal value As String)
                Me._Modello = value
            End Set
        End Property

        Public Property MATRICOLA() As String
            Get
                Return _Matricola
            End Get
            Set(ByVal value As String)
                Me._Matricola = value
            End Set
        End Property


        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property

        Public Property ANNO_COSTRUZIONE() As String
            Get
                Return _Anno
            End Get
            Set(ByVal value As String)
                Me._Anno = value
            End Set
        End Property

        Public Property POTENZA() As Double
            Get
                Return _Potenza
            End Get
            Set(ByVal value As Double)
                Me._Potenza = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal modello As String, ByVal matricola As String, ByVal note As String, ByVal anno As String, ByVal potenza As Double)

            Me.ID = id
            Me.MODELLO = modello
            Me.MATRICOLA = matricola
            Me.NOTE = note
            Me.ANNO_COSTRUZIONE = anno
            Me.POTENZA = potenza
        End Sub

    End Class

    '*** IMP_TERMICO CONTROLLO RENDIMENTO COMBUSTIBILE [RENDIMENTO_TERMICI]
    Public Class ControlloRendimento
        Private _id As Integer

        Private _Data As String
        Private _Esecutore As String
        Private _TempiFumi As Double
        Private _TempiAmb As Double
        Private _O2 As Double
        Private _CO2 As Double
        Private _BACHARACH As Double
        Private _CO As Double
        Private _Rendimento As Double
        Private _Tiraggio As Double



        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property DATA_ESAME() As String
            Get
                Return _Data
            End Get
            Set(ByVal value As String)
                Me._Data = value
            End Set
        End Property

        Public Property ESECUTORE() As String
            Get
                Return _Esecutore
            End Get
            Set(ByVal value As String)
                Me._Esecutore = value
            End Set
        End Property

        Public Property TEMP_FUMI() As Double
            Get
                Return _TempiFumi
            End Get
            Set(ByVal value As Double)
                Me._TempiFumi = value
            End Set
        End Property

        Public Property TEMP_AMB() As Double
            Get
                Return _TempiAmb
            End Get
            Set(ByVal value As Double)
                Me._TempiAmb = value
            End Set
        End Property

        Public Property O2() As Double
            Get
                Return _O2
            End Get
            Set(ByVal value As Double)
                Me._O2 = value
            End Set
        End Property

        Public Property CO2() As Double
            Get
                Return _CO2
            End Get
            Set(ByVal value As Double)
                Me._CO2 = value
            End Set
        End Property

        Public Property BACHARACH() As Double
            Get
                Return _BACHARACH
            End Get
            Set(ByVal value As Double)
                Me._BACHARACH = value
            End Set
        End Property

        Public Property CO() As Double
            Get
                Return _CO
            End Get
            Set(ByVal value As Double)
                Me._CO = value
            End Set
        End Property

        Public Property RENDIMENTO() As Double
            Get
                Return _Rendimento
            End Get
            Set(ByVal value As Double)
                Me._Rendimento = value
            End Set
        End Property

        Public Property TIRAGGIO() As Double
            Get
                Return _Tiraggio
            End Get
            Set(ByVal value As Double)
                Me._Tiraggio = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal data As String, ByVal esecutore As String, ByVal tempifumi As Double, ByVal tempiamb As Double, ByVal o2 As Double, ByVal co2 As Double, ByVal BACHARACH As Double, ByVal co As Double, ByVal rendimento As Double, ByVal tiraggio As Double)

            Me.ID = id
            Me.DATA_ESAME = data
            Me.ESECUTORE = esecutore
            Me.TEMP_FUMI = tempifumi
            Me.TEMP_AMB = tempiamb
            Me.O2 = o2
            Me.CO2 = co2
            Me.BACHARACH = BACHARACH
            Me.CO = co
            Me.RENDIMENTO = rendimento
            Me.TIRAGGIO = tiraggio
        End Sub

    End Class

    '*** IMP_TERMICO ELENCO EDIFICI ALIMENTATI [IMPIANTI_EDIFICI]
    Public Class Edifici
        Private _id As Integer

        Private _Denominazione As String
        Private _TotUnita As Integer
        Private _DimensioneUnita As Double
        Private _Descrizione As String
        Private _DescrizioneNO_MQ As String

        Public ReadOnly Property Descrizione() As String
            Get
                Return Me.ToString()
            End Get
        End Property

        Public ReadOnly Property DescrizioneNO_MQ() As String
            Get
                Return Me.ToString2()
            End Get
        End Property

        Public Overrides Function ToString() As String
            Dim s As String = ""
            Dim sb As New StringBuilder()


            sb.AppendFormat("{0}{1}{2}", Me.DENOMINAZIONE, vbTab, vbTab)
            sb.AppendFormat("- -U.I.={0}{1}{2}", Me.TOT_UNITA.ToString(), vbTab, vbTab)
            sb.AppendFormat("- -MQ={0}{1}", Me.DIMENSIONE.ToString(), vbTab)


            's = string.Format("DENOMINAZIONE {0}{1}{2} DENOMINAZIONE & " -- U.I.=" & TOT_UNITA & " -- Mq=" & DIMENSIONE

            Return sb.ToString()
        End Function

        Public Function ToString2() As String
            Dim s As String = ""
            Dim sb As New StringBuilder()


            sb.AppendFormat("{0}{1}{2}", Me.DENOMINAZIONE, vbTab, vbTab)
            sb.AppendFormat("- -U.I.={0}{1}{2}", Me.TOT_UNITA.ToString(), vbTab, vbTab)

            Return sb.ToString()
        End Function

        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property DENOMINAZIONE() As String
            Get
                Return _Denominazione
            End Get
            Set(ByVal value As String)
                Me._Denominazione = value
            End Set
        End Property

        Public Property TOT_UNITA() As Integer
            Get
                Return _TotUnita
            End Get
            Set(ByVal value As Integer)
                Me._TotUnita = value
            End Set
        End Property


        Public Property DIMENSIONE() As Double
            Get
                Return _DimensioneUnita
            End Get
            Set(ByVal value As Double)
                Me._DimensioneUnita = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal denominazione As String, ByVal tot_unita As Integer, ByVal dimensione As Double)

            Me.ID = id
            Me.DENOMINAZIONE = denominazione
            Me.TOT_UNITA = tot_unita
            Me.DIMENSIONE = dimensione

        End Sub

    End Class

    '*** IMP_IDRICO Elenco EDIFICI e/o SCALE SERVITE [IMPIANTI_EDIFICI_SCALE]
    Public Class EdificiScale
        Private _id As Integer
        Private _idScala As Integer

        Private _Denominazione As String
        Private _Scala As String
        Private _TotUnita As Integer
        Private _DimensioneUnita As Double
        Private _Descrizione As String
        Private _DescrizioneNO_MQ As String
        Private _DescrizioneScala As String
        Private _DescrizioneScalaNO_MQ As String

        Public ReadOnly Property Descrizione() As String
            Get
                Return Me.ToString() 'DENOMINAZIONE,TOT_UNITA,DIMENSIONE
            End Get
        End Property

        Public ReadOnly Property DescrizioneNO_MQ() As String
            Get
                Return Me.ToString2() 'DENOMINAZIONE,TOT_UNITA
            End Get
        End Property

        Public ReadOnly Property DescrizioneScala() As String
            Get
                Return Me.ToString3() 'DENOMINAZIONE,SCALA,TOT_UNITA,DIMENSIONE
            End Get
        End Property

        Public ReadOnly Property DescrizioneScalaNO_MQ() As String
            Get
                Return Me.ToString4() 'DENOMINAZIONE,SCALA,TOT_UNITA
            End Get
        End Property

        Public Overrides Function ToString() As String
            Dim s As String = ""
            Dim sb As New StringBuilder()


            sb.AppendFormat("{0}{1}{2}", Me.DENOMINAZIONE, vbTab, vbTab)
            sb.AppendFormat("- -U.I.={0}{1}{2}", Me.TOT_UNITA.ToString(), vbTab, vbTab)
            sb.AppendFormat("- -MQ={0}{1}", Me.DIMENSIONE.ToString(), vbTab)


            's = string.Format("DENOMINAZIONE {0}{1}{2} DENOMINAZIONE & " -- U.I.=" & TOT_UNITA & " -- Mq=" & DIMENSIONE

            Return sb.ToString()
        End Function

        Public Function ToString2() As String
            Dim s As String = ""
            Dim sb As New StringBuilder()


            sb.AppendFormat("{0}{1}{2}", Me.DENOMINAZIONE, vbTab, vbTab)
            sb.AppendFormat("- -U.I.={0}{1}", Me.TOT_UNITA.ToString(), vbTab)

            Return sb.ToString()
        End Function

        Public Function ToString3() As String
            Dim s As String = ""
            Dim sb As New StringBuilder()


            sb.AppendFormat("{0}{1}{2}", Me.DENOMINAZIONE, vbTab, vbTab)
            sb.AppendFormat(" - {0}{1}{2}", Me.SCALA, vbTab, vbTab)
            sb.AppendFormat("- -U.I.={0}{1}{2}", Me.TOT_UNITA.ToString(), vbTab, vbTab)
            sb.AppendFormat("- -MQ={0}{1}", Me.DIMENSIONE.ToString(), vbTab)

            Return sb.ToString()
        End Function

        Public Function ToString4() As String
            Dim s As String = ""
            Dim sb As New StringBuilder()


            sb.AppendFormat("{0}{1}{2}", Me.DENOMINAZIONE, vbTab, vbTab)
            sb.AppendFormat(" - {0}{1}{2}", Me.SCALA, vbTab, vbTab)
            sb.AppendFormat("- -U.I.={0}{1}", Me.TOT_UNITA.ToString(), vbTab)

            Return sb.ToString()
        End Function

        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property ID_SCALA() As Integer
            Get
                Return _idScala
            End Get
            Set(ByVal value As Integer)
                Me._idScala = value
            End Set
        End Property

        Public Property DENOMINAZIONE() As String
            Get
                Return _Denominazione
            End Get
            Set(ByVal value As String)
                Me._Denominazione = value
            End Set
        End Property

        Public Property SCALA() As String
            Get
                Return _Scala
            End Get
            Set(ByVal value As String)
                Me._Scala = value
            End Set
        End Property

        Public Property TOT_UNITA() As Integer
            Get
                Return _TotUnita
            End Get
            Set(ByVal value As Integer)
                Me._TotUnita = value
            End Set
        End Property


        Public Property DIMENSIONE() As Double
            Get
                Return _DimensioneUnita
            End Get
            Set(ByVal value As Double)
                Me._DimensioneUnita = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal idScala As Integer, ByVal denominazione As String, ByVal scala As String, ByVal tot_unita As Integer, ByVal dimensione As Double)

            Me.ID = id
            Me.ID_SCALA = idScala
            Me.DENOMINAZIONE = denominazione
            Me.SCALA = scala
            Me.TOT_UNITA = tot_unita
            Me.DIMENSIONE = dimensione

        End Sub

    End Class

    '*** IMP_TERMICO ELENCO EDIFICI ALIMENTATI 
    Public Class Scale
        Private _id As Integer

        Private _DenominazioneEdificio As String
        Private _DenominazioneScale As String


        Public ReadOnly Property SCALE() As String
            Get
                Return Me.ToString()
            End Get
        End Property

        Public ReadOnly Property SCALE_NO_TITLE() As String
            Get
                Return Me.ToString3()
            End Get
        End Property

        Public ReadOnly Property EDIFICI() As String
            Get
                Return Me.ToString2()
            End Get
        End Property

        Public Overrides Function ToString() As String
            Dim s As String = ""
            Dim sb As New StringBuilder()


            sb.AppendFormat("{0}{1}{2}", Me.DENOMINAZIONE_EDIFICIO.ToString, vbTab, vbTab)
            sb.AppendFormat("- -SCALA={0}{1}{2}", Me.DENOMINAZIONE_SCALA.ToString(), vbTab, vbTab)

            Return sb.ToString()
        End Function

        Public Function ToString2() As String
            Dim s As String = ""
            Dim sb As New StringBuilder()


            sb.AppendFormat("{0}{1}{2}", Me.DENOMINAZIONE_EDIFICIO.ToString, vbTab, vbTab)
            'sb.AppendFormat("- -SCALA={0}{1}{2}", Me.DENOMINAZIONE_SCALA.ToString(), vbTab, vbTab)

            Return sb.ToString()
        End Function

        Public Function ToString3() As String
            Dim s As String = ""
            Dim sb As New StringBuilder()

            sb.AppendFormat("{0}{1}{2}", Me.DENOMINAZIONE_SCALA.ToString(), vbTab, vbTab)

            Return sb.ToString()
        End Function

        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property DENOMINAZIONE_EDIFICIO() As String
            Get
                Return _DenominazioneEdificio
            End Get
            Set(ByVal value As String)
                Me._DenominazioneEdificio = value
            End Set
        End Property

        Public Property DENOMINAZIONE_SCALA() As String
            Get
                Return _DenominazioneScale
            End Get
            Set(ByVal value As String)
                Me._DenominazioneScale = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal denominazioneEdificio As String, ByVal denominazioneScala As String)

            Me.ID = id
            Me.DENOMINAZIONE_EDIFICIO = denominazioneEdificio
            Me.DENOMINAZIONE_SCALA = denominazioneScala

        End Sub
    End Class

    '#####

    '*** POMPE DI SOLLEVAMENTO 
    '    x IMP_TERMICO [I_TER_POMPE_SOLLEVAMENTO] 
    '    x IMP_IDRICO [POMPE_CIRCOLAZIONE_IDRICI]
    Public Class PompeS
        Private _id As Integer

        Private _Modello As String
        Private _Matricola As String
        Private _Anno As String
        Private _Potenza As Double
        Private _Portata As Double
        Private _Prevalenza As Double
        Private _Disconnettore As String



        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property MODELLO() As String
            Get
                Return _Modello
            End Get
            Set(ByVal value As String)
                Me._Modello = value
            End Set
        End Property

        Public Property MATRICOLA() As String
            Get
                Return _Matricola
            End Get
            Set(ByVal value As String)
                Me._Matricola = value
            End Set
        End Property

        Public Property ANNO_COSTRUZIONE() As String
            Get
                Return _Anno
            End Get
            Set(ByVal value As String)
                Me._Anno = value
            End Set
        End Property

        Public Property POTENZA() As Double
            Get
                Return _Potenza
            End Get
            Set(ByVal value As Double)
                Me._Potenza = value
            End Set
        End Property

        Public Property PORTATA() As Double
            Get
                Return _Portata
            End Get
            Set(ByVal value As Double)
                Me._Portata = value
            End Set
        End Property

        Public Property PREVALENZA() As Double
            Get
                Return _Prevalenza
            End Get
            Set(ByVal value As Double)
                Me._Prevalenza = value
            End Set
        End Property

        Public Property DISCONNETTORE() As String
            Get
                Return _Disconnettore
            End Get
            Set(ByVal value As String)
                Me._Disconnettore = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal modello As String, ByVal matricola As String, ByVal anno As String, ByVal potenza As Double, ByVal portata As Double, ByVal prevalenza As Double, ByVal disconnettore As String)

            Me.ID = id
            Me.MODELLO = modello
            Me.MATRICOLA = matricola
            Me.ANNO_COSTRUZIONE = anno
            Me.POTENZA = potenza
            Me.PORTATA = portata
            Me.PREVALENZA = prevalenza
            Me.DISCONNETTORE = disconnettore
        End Sub

    End Class


    '*** IMP_IDRICO SERBATOI AUTOCLAVE  [SERBATOI_IDRICI]
    Public Class Serbatoi
        Private _id As Integer

        Private _Modello As String
        Private _Matricola As String
        Private _Anno As String
        Private _Volume As Double
        Private _PressioneBolla As Double
        Private _PressioneEsercizio As Double
        Private _Note As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property MODELLO() As String
            Get
                Return _Modello
            End Get
            Set(ByVal value As String)
                Me._Modello = value
            End Set
        End Property

        Public Property MATRICOLA() As String
            Get
                Return _Matricola
            End Get
            Set(ByVal value As String)
                Me._Matricola = value
            End Set
        End Property


        Public Property ANNO_COSTRUZIONE() As String
            Get
                Return _Anno
            End Get
            Set(ByVal value As String)
                Me._Anno = value
            End Set
        End Property

        Public Property VOLUME() As Double
            Get
                Return _Volume
            End Get
            Set(ByVal value As Double)
                Me._Volume = value
            End Set
        End Property

        Public Property PRESSIONE_BOLLA() As Double
            Get
                Return _PressioneBolla
            End Get
            Set(ByVal value As Double)
                Me._PressioneBolla = value
            End Set
        End Property

        Public Property PRESSIONE_ESERCIZIO() As Double
            Get
                Return _PressioneEsercizio
            End Get
            Set(ByVal value As Double)
                Me._PressioneEsercizio = value
            End Set
        End Property

        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal modello As String, ByVal matricola As String, ByVal anno As String, ByVal volume As Double, ByVal pressione_b As Double, ByVal pressione_e As Double, ByVal note As String)

            Me.ID = id
            Me.MODELLO = modello
            Me.MATRICOLA = matricola
            Me.ANNO_COSTRUZIONE = anno
            Me.VOLUME = volume
            Me.PRESSIONE_BOLLA = pressione_b
            Me.PRESSIONE_ESERCIZIO = pressione_e
            Me.NOTE = note

        End Sub

    End Class

    '#####

    '*** I_ELETTRICO QUADRO DI PORTINERIA [I_ELE_PORTINERIA]
    Public Class Portineria
        Private _id As Integer

        Private _Quadro As String
        Private _Differenziale As String
        Private _Norma As String
        Private _Distribuzione As String
        Private _ID_Distribuzione As Integer
        Private _Note As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property QUADRO() As String
            Get
                Return _Quadro
            End Get
            Set(ByVal value As String)
                Me._Quadro = value
            End Set
        End Property

        Public Property DIFFERENZIALE() As String
            Get
                Return _Differenziale
            End Get
            Set(ByVal value As String)
                Me._Differenziale = value
            End Set
        End Property


        Public Property NORMA() As String
            Get
                Return _Norma
            End Get
            Set(ByVal value As String)
                Me._Norma = value
            End Set
        End Property

        Public Property DISTRIBUZIONE() As String
            Get
                Return _Distribuzione
            End Get
            Set(ByVal value As String)
                Me._Distribuzione = value
            End Set
        End Property

        Public Property ID_TIPO_DISTRIBUZIONE() As Integer
            Get
                Return _ID_Distribuzione
            End Get
            Set(ByVal value As Integer)
                Me._ID_Distribuzione = value
            End Set
        End Property

        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal quadro As String, ByVal differenziale As String, ByVal norma As String, ByVal distribuzione As String, ByVal id_distribuzione As Integer, ByVal note As String)

            Me.ID = id
            Me.QUADRO = quadro
            Me.DIFFERENZIALE = differenziale
            Me.NORMA = norma
            Me.DISTRIBUZIONE = distribuzione
            Me.ID_TIPO_DISTRIBUZIONE = id_distribuzione
            Me.NOTE = note
        End Sub

    End Class

    '*** I_ELETTRICO BOX [I_ELE_BOX]
    Public Class Box
        Private _id As Integer

        Private _Auto As String
        Private _Quadro As String
        Private _Differenziale As String
        Private _Distribuzione As String
        Private _ID_Distribuzione As Integer

        Private _Sgancio As String
        Private _Pratica As String
        Private _Verifica As String
        Private _MessaTerra As String
        Private _Scariche As String
        Private _Scaricatori As String

        Private _Note As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property SUP_9_AUTO() As String
            Get
                Return _Auto
            End Get
            Set(ByVal value As String)
                Me._Auto = value
            End Set
        End Property

        Public Property QUADRO() As String
            Get
                Return _Quadro
            End Get
            Set(ByVal value As String)
                Me._Quadro = value
            End Set
        End Property

        Public Property DIFFERENZIALE() As String
            Get
                Return _Differenziale
            End Get
            Set(ByVal value As String)
                Me._Differenziale = value
            End Set
        End Property

        Public Property DISTRIBUZIONE() As String
            Get
                Return _Distribuzione
            End Get
            Set(ByVal value As String)
                Me._Distribuzione = value
            End Set
        End Property

        Public Property ID_TIPO_DISTRIBUZIONE() As Integer
            Get
                Return _ID_Distribuzione
            End Get
            Set(ByVal value As Integer)
                Me._ID_Distribuzione = value
            End Set
        End Property

        Public Property PULSANTE_SGANCIO() As String
            Get
                Return _Sgancio
            End Get
            Set(ByVal value As String)
                Me._Sgancio = value
            End Set
        End Property

        Public Property PRATICA_VVF() As String
            Get
                Return _Pratica
            End Get
            Set(ByVal value As String)
                Me._Pratica = value
            End Set
        End Property

        Public Property VERIFICA() As String
            Get
                Return _Verifica
            End Get
            Set(ByVal value As String)
                Me._Verifica = value
            End Set
        End Property


        Public Property MESSA_TERRA() As String
            Get
                Return _MessaTerra
            End Get
            Set(ByVal value As String)
                Me._MessaTerra = value
            End Set
        End Property

        Public Property SCARICHE_ATMOSFERICHE() As String
            Get
                Return _Scariche
            End Get
            Set(ByVal value As String)
                Me._Scariche = value
            End Set
        End Property

        Public Property SCARICATORI() As String
            Get
                Return _Scaricatori
            End Get
            Set(ByVal value As String)
                Me._Scaricatori = value
            End Set
        End Property


        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal auto As String, ByVal quadro As String, ByVal differenziale As String, ByVal distribuzione As String, ByVal id_distribuzione As Integer, ByVal sgancio As String, ByVal pratica As String, ByVal verifiche As String, ByVal messaterra As String, ByVal scariche As String, ByVal scaricatori As String, ByVal note As String)

            Me.ID = id
            Me.SUP_9_AUTO = auto
            Me.QUADRO = quadro
            Me.DIFFERENZIALE = differenziale
            Me.DISTRIBUZIONE = distribuzione
            Me.ID_TIPO_DISTRIBUZIONE = id_distribuzione
            Me.PULSANTE_SGANCIO = sgancio
            Me.PRATICA_VVF = pratica
            Me.VERIFICA = verifiche
            Me.MESSA_TERRA = messaterra
            Me.SCARICHE_ATMOSFERICHE = scariche
            Me.SCARICATORI = scaricatori
            Me.NOTE = note
        End Sub

    End Class

    '*** I_ELETTRICO QUADRO SERVIZI e SCALA [I_ELE_QUADRO_SERVIZI e I_ELE_QUADRO_SCALA]
    Public Class Quadro
        Private _id As Integer

        Private _Quantita As Integer
        Private _Differenziale As String
        Private _Norma As String
        Private _Ubicazione As String
        Private _ElementiServiti As Integer

        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property QUANTITA() As Integer
            Get
                Return _Quantita
            End Get
            Set(ByVal value As Integer)
                Me._Quantita = value
            End Set
        End Property


        Public Property DIFFERENZIALE() As String
            Get
                Return _Differenziale
            End Get
            Set(ByVal value As String)
                Me._Differenziale = value
            End Set
        End Property

        Public Property NORMA() As String
            Get
                Return _Norma
            End Get
            Set(ByVal value As String)
                Me._Norma = value
            End Set
        End Property

        Public Property UBICAZIONE() As String
            Get
                Return _Ubicazione
            End Get
            Set(ByVal value As String)
                Me._Ubicazione = value
            End Set
        End Property

        Public Property SCALE_SERVITE() As Integer
            Get
                Return _ElementiServiti
            End Get
            Set(ByVal value As Integer)
                Me._ElementiServiti = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal quantita As Integer, ByVal differenziale As String, ByVal norma As String, ByVal ubicazione As String, ByVal ElementiServiti As Integer)

            Me.ID = id
            Me.QUANTITA = quantita
            Me.DIFFERENZIALE = differenziale
            Me.NORMA = norma
            Me.UBICAZIONE = ubicazione
            Me.SCALE_SERVITE = ElementiServiti
        End Sub

    End Class


    '*** I_TV [I_TV_DETTAGLI]
    Public Class TV
        Private _id As Integer

        Private _id_Edificio As Integer
        Private _id_Scala As Integer

        Private _Edificio As String
        Private _Scala As String

        Private _Ditta As String
        Private _Data As String

        Private _Centralino As String
        Private _Impianto As String
        Private _TipoImpianto As String

        Private _Distribuzione As String
        Private _ID_Distribuzione As Integer

        Private _FabbServiti As Integer

        Private _Note As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property ID_UBICAZIONE_EDIFICIO() As Integer
            Get
                Return _id_Edificio
            End Get
            Set(ByVal value As Integer)
                Me._id_Edificio = value
            End Set
        End Property

        Public Property ID_UBICAZIONE_SCALA() As Integer
            Get
                Return _id_Scala
            End Get
            Set(ByVal value As Integer)
                Me._id_Scala = value
            End Set
        End Property

        Public Property EDIFICIO() As String
            Get
                Return _Edificio
            End Get
            Set(ByVal value As String)
                Me._Edificio = value
            End Set
        End Property

        Public Property SCALA() As String
            Get
                Return _Scala
            End Get
            Set(ByVal value As String)
                Me._Scala = value
            End Set
        End Property

        Public Property DITTA_INSTALLAZIONE() As String
            Get
                Return _Ditta
            End Get
            Set(ByVal value As String)
                Me._Ditta = value
            End Set
        End Property

        Public Property DATA_INSTALLAZIONE() As String
            Get
                Return _Data
            End Get
            Set(ByVal value As String)
                Me._Data = value
            End Set
        End Property

        Public Property CENTRALINO_TV() As String
            Get
                Return _Centralino
            End Get
            Set(ByVal value As String)
                Me._Centralino = value
            End Set
        End Property

        Public Property IMPIANTO() As String
            Get
                Return _Impianto
            End Get
            Set(ByVal value As String)
                Me._Impianto = value
            End Set
        End Property

        Public Property TIPO_IMPIANTO() As String
            Get
                Return _TipoImpianto
            End Get
            Set(ByVal value As String)
                Me._TipoImpianto = value
            End Set
        End Property

        Public Property DISTRIBUZIONE() As String
            Get
                Return _Distribuzione
            End Get
            Set(ByVal value As String)
                Me._Distribuzione = value
            End Set
        End Property

        Public Property ID_TIPO_DISTRIBUZIONE() As Integer
            Get
                Return _ID_Distribuzione
            End Get
            Set(ByVal value As Integer)
                Me._ID_Distribuzione = value
            End Set
        End Property

        Public Property FABB_SERVITI() As Integer
            Get
                Return _FabbServiti
            End Get
            Set(ByVal value As Integer)
                Me._FabbServiti = value
            End Set
        End Property

        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal idEdificio As Integer, ByVal idScala As Integer, ByVal edificio As String, ByVal scala As String, ByVal ditta As String, ByVal data As String, ByVal centralino As String, ByVal impianto As String, ByVal tipoimpianto As String, ByVal distribuzione As String, ByVal id_distribuzione As Integer, ByVal fabb_serviti As Integer, ByVal note As String)

            Me.ID = id
            Me.ID_UBICAZIONE_EDIFICIO = idEdificio
            Me.ID_UBICAZIONE_SCALA = idScala
            Me.EDIFICIO = edificio
            Me.SCALA = scala

            Me.DITTA_INSTALLAZIONE = ditta
            Me.DATA_INSTALLAZIONE = data

            Me.CENTRALINO_TV = centralino
            Me.IMPIANTO = impianto
            Me.TIPO_IMPIANTO = tipoimpianto
            Me.DISTRIBUZIONE = distribuzione
            Me.ID_TIPO_DISTRIBUZIONE = id_distribuzione
            Me.FABB_SERVITI = fabb_serviti
            Me.NOTE = note
        End Sub

    End Class

    '#####

    '*** POMPE DI SOLLEVAMENTO 
    '    x IMP_METEORICHE [I_MET_POMPE_SOLLEVAMENTO] 
    Public Class PompeSM
        Private _id As Integer

        Private _Modello As String
        Private _Matricola As String
        Private _Anno As String
        Private _Tipo As String
        Private _Potenza As Double
        Private _Portata As Double
        Private _Prevalenza As Double


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property MODELLO() As String
            Get
                Return _Modello
            End Get
            Set(ByVal value As String)
                Me._Modello = value
            End Set
        End Property

        Public Property MATRICOLA() As String
            Get
                Return _Matricola
            End Get
            Set(ByVal value As String)
                Me._Matricola = value
            End Set
        End Property

        Public Property ANNO_COSTRUZIONE() As String
            Get
                Return _Anno
            End Get
            Set(ByVal value As String)
                Me._Anno = value
            End Set
        End Property

        Public Property TIPO() As String
            Get
                Return _Tipo
            End Get
            Set(ByVal value As String)
                Me._Tipo = value
            End Set
        End Property

        Public Property POTENZA() As Double
            Get
                Return _Potenza
            End Get
            Set(ByVal value As Double)
                Me._Potenza = value
            End Set
        End Property

        Public Property PORTATA() As Double
            Get
                Return _Portata
            End Get
            Set(ByVal value As Double)
                Me._Portata = value
            End Set
        End Property

        Public Property PREVALENZA() As Double
            Get
                Return _Prevalenza
            End Get
            Set(ByVal value As Double)
                Me._Prevalenza = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal modello As String, ByVal matricola As String, ByVal anno As String, ByVal tipo As String, ByVal potenza As Double, ByVal portata As Double, ByVal prevalenza As Double)

            Me.ID = id
            Me.MODELLO = modello
            Me.MATRICOLA = matricola
            Me.ANNO_COSTRUZIONE = anno
            Me.TIPO = tipo
            Me.POTENZA = potenza
            Me.PORTATA = portata
            Me.PREVALENZA = prevalenza
        End Sub

    End Class

    '#####

    '*** I_ANTINCENDIO SERBATOI ACCUMULO [I_ANT_SERBATOI]
    Public Class SerbatoiAccumulo
        Private _id As Integer

        Private _id_Edificio As Integer
        Private _id_Scala As Integer

        Private _Edificio As String
        Private _Scala As String

        Private _Capacita As Double



        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property ID_UBICAZIONE_EDIFICIO() As Integer
            Get
                Return _id_Edificio
            End Get
            Set(ByVal value As Integer)
                Me._id_Edificio = value
            End Set
        End Property

        Public Property ID_UBICAZIONE_SCALA() As Integer
            Get
                Return _id_Scala
            End Get
            Set(ByVal value As Integer)
                Me._id_Scala = value
            End Set
        End Property

        Public Property EDIFICIO() As String
            Get
                Return _Edificio
            End Get
            Set(ByVal value As String)
                Me._Edificio = value
            End Set
        End Property

        Public Property SCALA() As String
            Get
                Return _Scala
            End Get
            Set(ByVal value As String)
                Me._Scala = value
            End Set
        End Property



        Public Property CAPACITA() As Double
            Get
                Return _Capacita
            End Get
            Set(ByVal value As Double)
                Me._Capacita = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal idEdificio As Integer, ByVal idScala As Integer, ByVal edificio As String, ByVal scala As String, ByVal capacita As Double)

            Me.ID = id
            Me.ID_UBICAZIONE_EDIFICIO = idEdificio
            Me.ID_UBICAZIONE_SCALA = idScala
            Me.EDIFICIO = edificio
            Me.SCALA = scala

            Me.CAPACITA = capacita

        End Sub

    End Class

    '*** I_ANTINCENDIO MOTOPOMPE UNI 70  [I_ANT_MOTOPOMPE]
    Public Class Motopompe
        Private _id As Integer

        Private _id_Edificio As Integer
        Private _id_Scala As Integer

        Private _Edificio As String
        Private _Scala As String

        Private _ScaleServite As Integer


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property ID_UBICAZIONE_EDIFICIO() As Integer
            Get
                Return _id_Edificio
            End Get
            Set(ByVal value As Integer)
                Me._id_Edificio = value
            End Set
        End Property

        Public Property ID_UBICAZIONE_SCALA() As Integer
            Get
                Return _id_Scala
            End Get
            Set(ByVal value As Integer)
                Me._id_Scala = value
            End Set
        End Property

        Public Property EDIFICIO() As String
            Get
                Return _Edificio
            End Get
            Set(ByVal value As String)
                Me._Edificio = value
            End Set
        End Property

        Public Property SCALA() As String
            Get
                Return _Scala
            End Get
            Set(ByVal value As String)
                Me._Scala = value
            End Set
        End Property



        Public Property SCALE_SERVITE() As Integer
            Get
                Return _ScaleServite
            End Get
            Set(ByVal value As Integer)
                Me._ScaleServite = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal idEdificio As Integer, ByVal idScala As Integer, ByVal edificio As String, ByVal scala As String, ByVal scale_servite As Integer)

            Me.ID = id
            Me.ID_UBICAZIONE_EDIFICIO = idEdificio
            Me.ID_UBICAZIONE_SCALA = idScala
            Me.EDIFICIO = edificio
            Me.SCALA = scala

            Me.SCALE_SERVITE = scale_servite

        End Sub

    End Class


    '*** I_ANTINCENDIO SPRINKLER  [I_ANT_SPRINKLER]
    Public Class Sprinkler
        Private _id As Integer

        Private _Allacciamento As String
        Private _Sprinkler As String
        Private _Certificazioni As String
        Private _Id_Sprinkler As Integer


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property ALLACCIAMENTO() As String
            Get
                Return _Allacciamento
            End Get
            Set(ByVal value As String)
                Me._Allacciamento = value
            End Set
        End Property

        Public Property SPRINKLER() As String
            Get
                Return _Sprinkler
            End Get
            Set(ByVal value As String)
                Me._Sprinkler = value
            End Set
        End Property

        Public Property CERTIFICAZIONI() As String
            Get
                Return _Certificazioni
            End Get
            Set(ByVal value As String)
                Me._Certificazioni = value
            End Set
        End Property

        Public Property ID_TIPOLOGIA_SPRINKLER() As Integer
            Get
                Return _Id_Sprinkler
            End Get
            Set(ByVal value As Integer)
                Me._Id_Sprinkler = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal allacciamento As String, ByVal sprinkler As String, ByVal certificazioni As String, ByVal id_sprinkler As Integer)

            Me.ID = id
            Me.ALLACCIAMENTO = allacciamento
            Me.SPRINKLER = sprinkler
            Me.CERTIFICAZIONI = certificazioni
            Me.ID_TIPOLOGIA_SPRINKLER = id_sprinkler
        End Sub

    End Class


    '*** I_ANTINCENDIO RILEVATORE FUMI  [I_ANT_RILEVAZIONE_FUMI]
    Public Class RilevatoreFumi
        Private _id As Integer

        Private _TipologiaFumi As String
        Private _Ubicazione As String
        Private _Id_TipologiaFumi As Integer


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property TIPOLOGIA_FUMI() As String
            Get
                Return _TipologiaFumi
            End Get
            Set(ByVal value As String)
                Me._TipologiaFumi = value
            End Set
        End Property

        Public Property UBICAZIONE_CENTRALINA() As String
            Get
                Return _Ubicazione
            End Get
            Set(ByVal value As String)
                Me._Ubicazione = value
            End Set
        End Property


        Public Property ID_TIPOLOGIA_RILEVAZIONE() As Integer
            Get
                Return _Id_TipologiaFumi
            End Get
            Set(ByVal value As Integer)
                Me._Id_TipologiaFumi = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal tipologiaFumi As String, ByVal ubicazione As String, ByVal id_tipologiaFumi As Integer)

            Me.ID = id
            Me.TIPOLOGIA_FUMI = tipologiaFumi
            Me.UBICAZIONE_CENTRALINA = ubicazione
            Me.ID_TIPOLOGIA_RILEVAZIONE = id_tipologiaFumi
        End Sub

    End Class

    '*** I_ANTINCENDIO IDRANTI/NASPI  [I_ANT_IDRANTI]
    Public Class Idranti
        Private _id As Integer

        Private _PianiServiti As Integer
        Private _diametro As Double
        Private _num_idranti As Integer
        Private _localizzazione As String

        Private _idVerifica As Integer
        Private _Ditta As String
        Private _DataVerifica As String
        Private _Note As String
        Private _EsitoDettaglio As String
        Private _MesiValidita As Integer
        Private _DataScadenza As String
        Private _Esito As Integer
        Private _PreAllarme As Integer
        Private _Tipo As String
        Private _Prescrizione As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property PIANI() As Integer
            Get
                Return _PianiServiti
            End Get
            Set(ByVal value As Integer)
                Me._PianiServiti = value
            End Set
        End Property

        Public Property DIAMETRO() As Double
            Get
                Return _diametro
            End Get
            Set(ByVal value As Double)
                Me._diametro = value
            End Set
        End Property

        Public Property NUM_IDRANTI() As Integer
            Get
                Return _num_idranti
            End Get
            Set(ByVal value As Integer)
                Me._num_idranti = value
            End Set
        End Property


        Public Property LOCALIZZAZIONE() As String
            Get
                Return _localizzazione
            End Get
            Set(ByVal value As String)
                Me._localizzazione = value
            End Set
        End Property

        Public Property ID_VERIFICA() As Integer
            Get
                Return _idVerifica
            End Get
            Set(ByVal value As Integer)
                Me._idVerifica = value
            End Set
        End Property

        Public Property DITTA() As String
            Get
                Return _Ditta
            End Get
            Set(ByVal value As String)
                Me._Ditta = value
            End Set
        End Property

        Public Property DATA() As String
            Get
                Return _DataVerifica
            End Get
            Set(ByVal value As String)
                Me._DataVerifica = value
            End Set
        End Property

        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property

        Public Property ESITO_DETTAGLIO() As String
            Get
                Return _EsitoDettaglio
            End Get
            Set(ByVal value As String)
                Me._EsitoDettaglio = value
            End Set
        End Property

        Public Property MESI_VALIDITA() As Integer
            Get
                Return _MesiValidita
            End Get
            Set(ByVal value As Integer)
                Me._MesiValidita = value
            End Set
        End Property

        Public Property DATA_SCADENZA() As String
            Get
                Return _DataScadenza
            End Get
            Set(ByVal value As String)
                Me._DataScadenza = value
            End Set
        End Property

        Public Property ESITO() As Integer
            Get
                Return _Esito
            End Get
            Set(ByVal value As Integer)
                Me._Esito = value
            End Set
        End Property

        Public Property MESI_PREALLARME() As Integer
            Get
                Return _PreAllarme
            End Get
            Set(ByVal value As Integer)
                Me._PreAllarme = value
            End Set
        End Property

        Public Property TIPO() As String
            Get
                Return _Tipo
            End Get
            Set(ByVal value As String)
                Me._Tipo = value
            End Set
        End Property

        Public Property ES_PRESCRIZIONE() As String
            Get
                Return _Prescrizione
            End Get
            Set(ByVal value As String)
                Me._Prescrizione = value
            End Set
        End Property



        Public Sub New(ByVal id As Integer, ByVal piani_serviti As Integer, ByVal diametro As Double, ByVal num_idranti As Integer, ByVal localizzazione As String, ByVal idVerifica As Integer, ByVal ditta As String, ByVal dataVerifica As String, ByVal note As String, ByVal esitodettaglio As String, ByVal Validita As Integer, ByVal dataScadenza As String, ByVal esito As Integer, ByVal PreAllarme As Integer, ByVal tipo As String, ByVal prescrizione As String)

            Me.ID = id
            Me.PIANI = piani_serviti
            Me.DIAMETRO = diametro
            Me.NUM_IDRANTI = num_idranti
            Me.LOCALIZZAZIONE = localizzazione

            Me.ID_VERIFICA = idVerifica
            Me.DITTA = ditta
            Me.DATA = dataVerifica
            Me.NOTE = note
            Me.ESITO_DETTAGLIO = esitodettaglio
            Me.MESI_VALIDITA = Validita
            Me.DATA_SCADENZA = dataScadenza
            Me.ESITO = esito
            Me.MESI_PREALLARME = PreAllarme
            Me.TIPO = tipo
            Me.ES_PRESCRIZIONE = prescrizione

        End Sub

    End Class

    '*** I_ANTINCENDIO ESTINTORI  [I_ANT_ESTINTORI]
    Public Class Estintori
        Private _id As Integer

        Private _Estintori As Integer

        Private _idVerifica As Integer
        Private _Ditta As String
        Private _DataVerifica As String
        Private _Note As String
        Private _EsitoDettaglio As String
        Private _MesiValidita As Integer
        Private _DataScadenza As String
        Private _Esito As Integer
        Private _PreAllarme As Integer
        Private _Tipo As String
        Private _Prescrizione As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property ESTINTORI() As Integer
            Get
                Return _Estintori
            End Get
            Set(ByVal value As Integer)
                Me._Estintori = value
            End Set
        End Property


        Public Property ID_VERIFICA() As Integer
            Get
                Return _idVerifica
            End Get
            Set(ByVal value As Integer)
                Me._idVerifica = value
            End Set
        End Property

        Public Property DITTA() As String
            Get
                Return _Ditta
            End Get
            Set(ByVal value As String)
                Me._Ditta = value
            End Set
        End Property

        Public Property DATA() As String
            Get
                Return _DataVerifica
            End Get
            Set(ByVal value As String)
                Me._DataVerifica = value
            End Set
        End Property

        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property

        Public Property ESITO_DETTAGLIO() As String
            Get
                Return _EsitoDettaglio
            End Get
            Set(ByVal value As String)
                Me._EsitoDettaglio = value
            End Set
        End Property

        Public Property MESI_VALIDITA() As Integer
            Get
                Return _MesiValidita
            End Get
            Set(ByVal value As Integer)
                Me._MesiValidita = value
            End Set
        End Property

        Public Property DATA_SCADENZA() As String
            Get
                Return _DataScadenza
            End Get
            Set(ByVal value As String)
                Me._DataScadenza = value
            End Set
        End Property

        Public Property ESITO() As Integer
            Get
                Return _Esito
            End Get
            Set(ByVal value As Integer)
                Me._Esito = value
            End Set
        End Property

        Public Property MESI_PREALLARME() As Integer
            Get
                Return _PreAllarme
            End Get
            Set(ByVal value As Integer)
                Me._PreAllarme = value
            End Set
        End Property

        Public Property TIPO() As String
            Get
                Return _Tipo
            End Get
            Set(ByVal value As String)
                Me._Tipo = value
            End Set
        End Property

        Public Property ES_PRESCRIZIONE() As String
            Get
                Return _Prescrizione
            End Get
            Set(ByVal value As String)
                Me._Prescrizione = value
            End Set
        End Property



        Public Sub New(ByVal id As Integer, ByVal estintori As Integer, ByVal idVerifica As Integer, ByVal ditta As String, ByVal dataVerifica As String, ByVal note As String, ByVal esitodettaglio As String, ByVal Validita As Integer, ByVal dataScadenza As String, ByVal esito As Integer, ByVal PreAllarme As Integer, ByVal tipo As String, ByVal prescrizione As String)

            Me.ID = id
            Me.ESTINTORI = estintori

            Me.ID_VERIFICA = idVerifica
            Me.DITTA = ditta
            Me.DATA = dataVerifica
            Me.NOTE = note
            Me.ESITO_DETTAGLIO = esitodettaglio
            Me.MESI_VALIDITA = Validita
            Me.DATA_SCADENZA = dataScadenza
            Me.ESITO = esito
            Me.MESI_PREALLARME = PreAllarme
            Me.TIPO = tipo
            Me.ES_PRESCRIZIONE = prescrizione

        End Sub

    End Class


    '*** I_ANTINCENDIO ATTACCO AUTOPOMPA  [I_ANT_AUTOPOMPA]
    Public Class AutoPompa
        Private _id As Integer

        Private _PianiServiti As Integer
        Private _Bocca As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property PIANI() As Integer
            Get
                Return _PianiServiti
            End Get
            Set(ByVal value As Integer)
                Me._PianiServiti = value
            End Set
        End Property

        Public Property BOCCA_COLLEGAMENTO() As String
            Get
                Return _Bocca
            End Get
            Set(ByVal value As String)
                Me._Bocca = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal piani_serviti As Integer, ByVal bocca As String)

            Me.ID = id
            Me.PIANI = piani_serviti
            Me.BOCCA_COLLEGAMENTO = bocca
        End Sub

    End Class

    '#####

    '*** I_TUTELA CANCELLI [I_TUT_CANCELLI]
    Public Class Cancelli
        Private _id As Integer
        Private _Marca As String
        Private _Modello As String
        Private _Automatizzato As String
        Private _Carrabile As String
        Private _Ditta As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property MARCA() As String
            Get
                Return _Marca
            End Get
            Set(ByVal value As String)
                Me._Marca = value
            End Set
        End Property


        Public Property MODELLO() As String
            Get
                Return _Modello
            End Get
            Set(ByVal value As String)
                Me._Modello = value
            End Set
        End Property

        Public Property AUTOMATIZZATO() As String
            Get
                Return _Automatizzato
            End Get
            Set(ByVal value As String)
                Me._Automatizzato = value
            End Set
        End Property

        Public Property CARRABILE() As String
            Get
                Return _Carrabile
            End Get
            Set(ByVal value As String)
                Me._Carrabile = value
            End Set
        End Property

        Public Property DITTA_MANUTENZIONE() As String
            Get
                Return _Ditta
            End Get
            Set(ByVal value As String)
                Me._Ditta = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal carrabile As String, ByVal automatizzato As String, ByVal marca As String, ByVal modello As String, ByVal ditta As String)

            Me.ID = id
            Me.CARRABILE = carrabile
            Me.AUTOMATIZZATO = automatizzato
            Me.MARCA = marca
            Me.MODELLO = modello
            Me.DITTA_MANUTENZIONE = ditta
        End Sub

    End Class

    '*** I_CITOFONI CITOFONI [I_CIT_DETTAGLI]
    Public Class Citofoni
        Private _id As Integer

        Private _Tipo As String
        Private _Ubicazione As String
        Private _Tastiera As String
        Private _Distribuzione As String
        Private _Id_Distribuzione As Integer
        Private _Quantita As Integer
        Private _ScaleServite As Integer


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property


        Public Property TIPOLOGIA() As String
            Get
                Return _Tipo
            End Get
            Set(ByVal value As String)
                Me._Tipo = value
            End Set
        End Property

        Public Property UBICAZIONE() As String
            Get
                Return _Ubicazione
            End Get
            Set(ByVal value As String)
                Me._Ubicazione = value
            End Set
        End Property

        Public Property TASTIERA() As String
            Get
                Return _Tastiera
            End Get
            Set(ByVal value As String)
                Me._Tastiera = value
            End Set
        End Property

        Public Property DISTRIBUZIONE() As String
            Get
                Return _Distribuzione
            End Get
            Set(ByVal value As String)
                Me._Distribuzione = value
            End Set
        End Property

        Public Property ID_TIPO_DISTRIBUZIONE() As Integer
            Get
                Return _Id_Distribuzione
            End Get
            Set(ByVal value As Integer)
                Me._Id_Distribuzione = value
            End Set
        End Property

        Public Property QUANTITA() As Integer
            Get
                Return _Quantita
            End Get
            Set(ByVal value As Integer)
                Me._Quantita = value
            End Set
        End Property


        Public Property SCALE_SERVITE() As Integer
            Get
                Return _ScaleServite
            End Get
            Set(ByVal value As Integer)
                Me._ScaleServite = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal tipo As String, ByVal ubicazione As String, ByVal tastiera As String, ByVal distribuzione As String, ByVal id_distribuzione As Integer, ByVal quantita As Integer, ByVal scale_servite As Integer)

            Me.ID = id
            Me.TIPOLOGIA = tipo
            Me.UBICAZIONE = ubicazione
            Me.TASTIERA = tastiera
            Me.DISTRIBUZIONE = distribuzione
            Me.ID_TIPO_DISTRIBUZIONE = id_distribuzione
            Me.QUANTITA = quantita
            Me.SCALE_SERVITE = scale_servite
        End Sub

    End Class

    '*** I_TUTELA ALLOGGI [I_TUT_ALLOGGI]
    Public Class Alloggi
        Private _id As Integer

        Private _Edificio As String
        Private _Scala As String
        Private _Piano As String
        Private _Interno As String
        Private _Nome_SUB As String

        Private _Antintruzione As String
        Private _Data_Inst_Antintruzione As String
        Private _Data_Rimozione_Antintruzione As String

        Private _Blindata As String
        Private _Data_Inst_Blindata As String

        Private _Lastratura As String
        Private _Data_Inst_Lastratura As String
        Private _Data_Rimozione_Lastratura As String

        Public _IDUnitaImmobiliare As Integer

        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property EDIFICIO() As String
            Get
                Return _Edificio
            End Get
            Set(ByVal value As String)
                Me._Edificio = value
            End Set
        End Property

        Public Property SCALA() As String
            Get
                Return _Scala
            End Get
            Set(ByVal value As String)
                Me._Scala = value
            End Set
        End Property

        Public Property PIANO() As String
            Get
                Return _Piano
            End Get
            Set(ByVal value As String)
                Me._Piano = value
            End Set
        End Property

        Public Property INTERNO() As String
            Get
                Return _Interno
            End Get
            Set(ByVal value As String)
                Me._Interno = value
            End Set
        End Property


        Public Property NOME_SUB() As String
            Get
                Return _Nome_SUB
            End Get
            Set(ByVal value As String)
                Me._Nome_SUB = value
            End Set
        End Property

        Public Property ANTINTRUSIONE() As String
            Get
                Return _Antintruzione
            End Get
            Set(ByVal value As String)
                Me._Antintruzione = value
            End Set
        End Property


        Public Property DATA_INSTALLA_ANTINTR() As String
            Get
                Return _Data_Inst_Antintruzione
            End Get
            Set(ByVal value As String)
                Me._Data_Inst_Antintruzione = value
            End Set
        End Property

        Public Property DATA_RIMOZIONE_ANTINTR() As String
            Get
                Return _Data_Rimozione_Antintruzione
            End Get
            Set(ByVal value As String)
                Me._Data_Rimozione_Antintruzione = value
            End Set
        End Property

        Public Property BLINDATA() As String
            Get
                Return _Blindata
            End Get
            Set(ByVal value As String)
                Me._Blindata = value
            End Set
        End Property


        Public Property DATA_INSTALLA_BLINDATA() As String
            Get
                Return _Data_Inst_Blindata
            End Get
            Set(ByVal value As String)
                Me._Data_Inst_Blindata = value
            End Set
        End Property

        Public Property LASTRATURA() As String
            Get
                Return _Lastratura
            End Get
            Set(ByVal value As String)
                Me._Lastratura = value
            End Set
        End Property


        Public Property DATA_INSTALLA_LASTRATURA() As String
            Get
                Return _Data_Inst_Lastratura
            End Get
            Set(ByVal value As String)
                Me._Data_Inst_Lastratura = value
            End Set
        End Property

        Public Property DATA_RIMOZIONE_LASTRATURA() As String
            Get
                Return _Data_Rimozione_Lastratura
            End Get
            Set(ByVal value As String)
                Me._Data_Rimozione_Lastratura = value
            End Set
        End Property

        Public Property ID_UNITA_IMMOBILIARI() As Integer
            Get
                Return _IDUnitaImmobiliare
            End Get
            Set(ByVal value As Integer)
                Me._IDUnitaImmobiliare = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal Edificio As String, ByVal Scala As String, ByVal Piano As String, ByVal Interno As String, ByVal Nome_Sub As String, ByVal Anti As String, ByVal Data1_Ant As String, ByVal Data2_Ant As String, ByVal Blindata As String, ByVal Data1_Blindata As String, ByVal Lastratura As String, ByVal Data1_Lastratura As String, ByVal Data2_Lastratura As String, ByVal IDUnitaImmobiliare As Integer)

            Me.ID = id
            Me.EDIFICIO = Edificio
            Me.SCALA = Scala
            Me.PIANO = Piano
            Me.INTERNO = Interno
            Me.NOME_SUB = Nome_Sub

            Me.ANTINTRUSIONE = Anti
            Me.DATA_INSTALLA_ANTINTR = Data1_Ant
            Me.DATA_RIMOZIONE_ANTINTR = Data2_Ant

            Me.BLINDATA = Blindata
            Me.DATA_INSTALLA_BLINDATA = Data1_Blindata

            Me.LASTRATURA = Lastratura
            Me.DATA_INSTALLA_LASTRATURA = Data1_Lastratura
            Me.DATA_RIMOZIONE_LASTRATURA = Data2_Lastratura

            Me.ID_UNITA_IMMOBILIARI = IDUnitaImmobiliare

        End Sub
    End Class


    '*** I_TUTELA PASSI CARRABILI [I_TUT_CARRABILE]
    Public Class Carrabile
        Private _id As Integer

        Private _Num_Licenza As String
        Private _Data_Rilascio As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property NUM_LICENZA() As String
            Get
                Return _Num_Licenza
            End Get
            Set(ByVal value As String)
                Me._Num_Licenza = value
            End Set
        End Property

        Public Property DATA_RILASCIO() As String
            Get
                Return _Data_Rilascio
            End Get
            Set(ByVal value As String)
                Me._Data_Rilascio = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal num_licenza As String, ByVal data_rilascio As String)

            Me.ID = id
            Me.NUM_LICENZA = num_licenza
            Me.DATA_RILASCIO = data_rilascio
        End Sub

    End Class

    '#####

    '*** VERIFICHE IMPIANTI [IMPIANTI_VERIFICHE]
    '*** I_ANTINCENDIO + I_TUTELA + I_SOLLEVAMENTO
    Public Class VerificheImpianti
        Private _id As Integer

        Private _Ditta As String
        Private _DataVerifica As String
        Private _Note As String
        Private _EsitoDettaglio As String
        Private _MesiValidita As Integer
        Private _DataScadenza As String
        Private _Esito As Integer
        Private _PreAllarme As Integer
        Private _Tipo As String
        Private _Prescrizione As String


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property DITTA() As String
            Get
                Return _Ditta
            End Get
            Set(ByVal value As String)
                Me._Ditta = value
            End Set
        End Property

        Public Property DATA() As String
            Get
                Return _DataVerifica
            End Get
            Set(ByVal value As String)
                Me._DataVerifica = value
            End Set
        End Property

        Public Property NOTE() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                Me._Note = value
            End Set
        End Property

        Public Property ESITO_DETTAGLIO() As String
            Get
                Return _EsitoDettaglio
            End Get
            Set(ByVal value As String)
                Me._EsitoDettaglio = value
            End Set
        End Property

        Public Property MESI_VALIDITA() As Integer
            Get
                Return _MesiValidita
            End Get
            Set(ByVal value As Integer)
                Me._MesiValidita = value
            End Set
        End Property

        Public Property DATA_SCADENZA() As String
            Get
                Return _DataScadenza
            End Get
            Set(ByVal value As String)
                Me._DataScadenza = value
            End Set
        End Property

        Public Property ESITO() As Integer
            Get
                Return _Esito
            End Get
            Set(ByVal value As Integer)
                Me._Esito = value
            End Set
        End Property

        Public Property MESI_PREALLARME() As Integer
            Get
                Return _PreAllarme
            End Get
            Set(ByVal value As Integer)
                Me._PreAllarme = value
            End Set
        End Property

        Public Property TIPO() As String
            Get
                Return _Tipo
            End Get
            Set(ByVal value As String)
                Me._Tipo = value
            End Set
        End Property

        Public Property ES_PRESCRIZIONE() As String
            Get
                Return _Prescrizione
            End Get
            Set(ByVal value As String)
                Me._Prescrizione = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal ditta As String, ByVal dataVerifica As String, ByVal note As String, ByVal esitodettaglio As String, ByVal Validita As Integer, ByVal dataScadenza As String, ByVal esito As Integer, ByVal PreAllarme As Integer, ByVal tipo As String, ByVal prescrizione As String)

            Me.ID = id
            Me.DITTA = ditta
            Me.DATA = dataVerifica
            Me.NOTE = note
            Me.ESITO_DETTAGLIO = esitodettaglio
            Me.MESI_VALIDITA = Validita
            Me.DATA_SCADENZA = dataScadenza
            Me.ESITO = esito
            Me.MESI_PREALLARME = PreAllarme
            Me.TIPO = tipo
            Me.ES_PRESCRIZIONE = prescrizione

        End Sub

    End Class


    '*** ELENCO VERIFICHE NELLE RICERCHE
    Public Class VerificheRicerche
        Private _id_impianto As Integer
        Private _id_verifiche As Integer

        Private _cod_complesso As String
        Private _complesso As String
        Private _edificio As String
        Private _tipo_impianto As String
        Private _tipo_verifica As String

        Private _DataVerifica As String
        Private _Validita As Integer
        Private _DataScadenza As String
        Private _codice_impianto As String


        Public Property ID_IMPIANTO() As Integer
            Get
                Return _id_impianto
            End Get
            Set(ByVal value As Integer)
                Me._id_impianto = value
            End Set
        End Property

        Public Property ID_VERIFICHE() As Integer
            Get
                Return _id_verifiche
            End Get
            Set(ByVal value As Integer)
                Me._id_verifiche = value
            End Set
        End Property


        Public Property COD_COMPLESSO() As String
            Get
                Return _cod_complesso
            End Get
            Set(ByVal value As String)
                Me._cod_complesso = value
            End Set
        End Property

        Public Property DENOMINAZIONE_COMPLESSO() As String
            Get
                Return _complesso
            End Get
            Set(ByVal value As String)
                Me._complesso = value
            End Set
        End Property

        Public Property DENOMINAZIONE_EDIFICIO() As String
            Get
                Return _edificio
            End Get
            Set(ByVal value As String)
                Me._edificio = value
            End Set
        End Property

        Public Property TIPO_IMPIANTO() As String
            Get
                Return _tipo_impianto
            End Get
            Set(ByVal value As String)
                Me._tipo_impianto = value
            End Set
        End Property

        Public Property TIPO_VERIFICA() As String
            Get
                Return _tipo_verifica
            End Get
            Set(ByVal value As String)
                Me._tipo_verifica = value
            End Set
        End Property

        Public Property DATA_VERIFICA() As String
            Get
                Return _DataVerifica
            End Get
            Set(ByVal value As String)
                Me._DataVerifica = value
            End Set
        End Property

        Public Property VALIDITA() As Integer
            Get
                Return _Validita
            End Get
            Set(ByVal value As Integer)
                Me._Validita = value
            End Set
        End Property

        Public Property SCADENZA() As String
            Get
                Return _DataScadenza
            End Get
            Set(ByVal value As String)
                Me._DataScadenza = value
            End Set
        End Property

        Public Property CODICE_IMPIANTO() As String
            Get
                Return _codice_impianto
            End Get
            Set(ByVal value As String)
                Me._codice_impianto = value
            End Set
        End Property

        Public Sub New(ByVal id_impianto As Integer, ByVal id_verifica As Integer, ByVal cod_complesso As String, ByVal complesso As String, ByVal edificio As String, ByVal tipo_impianto As String, ByVal tipo_verifica As String, ByVal dataVerifica As String, ByVal Validita As Integer, ByVal dataScadenza As String, ByVal cod_impianto As String)

            Me.ID_IMPIANTO = id_impianto
            Me.ID_VERIFICHE = id_verifica

            Me.COD_COMPLESSO = cod_complesso
            Me.DENOMINAZIONE_COMPLESSO = complesso
            Me.DENOMINAZIONE_EDIFICIO = edificio
            Me.TIPO_IMPIANTO = tipo_impianto
            Me.TIPO_VERIFICA = tipo_verifica

            Me.DATA_VERIFICA = dataVerifica
            Me.VALIDITA = Validita
            Me.SCADENZA = dataScadenza
            Me.CODICE_IMPIANTO = cod_impianto

        End Sub

    End Class




    '***********FINE IMPIANTI

    Public Enum TabEventi

        'IMPIANTI
        INSERIMENTO_DATI_IMPIANTO = 46
        MODIFICA_DATI_IMPIANTO = 47
        STAMPA_IMPIANTO = 48

        INSERIMENTO_DETTAGLIO_IMPIANTO = 49
        MODIFICA_DETTAGLIO_IMPIANTO = 50
        CANCELLAZIONE_DETTAGLIO_IMPIANTO = 51

        INSERIMENTO_VERIFICA_IMPIANTO = 52
        MODIFICA_VERIFICA_IMPIANTO = 53
        CANCELLAZIONE_VERIFICA_IMPIANTO = 54

        'MANUTENZIONE
        INSERIMENTO_MANUTENZIONE = 91
        MODIFICA_STATO_MANUTENZIONE = 92
        STAMPA_ORDINE_MANUTENZIONE = 93

        INSERIMENTO_DETTAGLI_MANUTENZIONE = 94
        MODIFICA_DETTAGLI_MANUTENZIONE = 95
        CANCELLAZIONE_DETTAGLI_MANUTENZIONE = 96


    End Enum

    Public Enum TabEventiMorosita

        'MOROSITA
        MESSA_IN_MORA = 0

        POSTALER_INDIRIZZO_INSUFFICIENTE = 1
        POSTALER_RECAPITATA_IN_SLA = 2
        POSTALER_COMPIUTA_GIACENZA = 3
        POSTALER_RITIRATA_DAL_DESTINATARIO = 4
        POSTALER_ASSENTE_INIZIO_GIACENZA = 5
        POSTALER_SCONOSCIUTO = 6
        POSTALER_TRASFERITO = 7
        POSTALER_CIVICO_INESISTENTE = 8
        POSTALER_DECEDUTO = 9
        POSTALER_RESPINTO = 10
        POSTALER_VIA_INESISTENTE = 11
        POSTALER_IMPOSSIBILE_RECAPITO = 12
        POSTALER_POSTALIZZATA = 13

        DILAZIONE = 14

        SOSPENSIONE_REVISIONE_CANONE = 15
        SOSPENSIONE_CONTRIBUTO_SOLIDARIETA = 16
        SOSPENSIONE_CONGRUITA_DEBITO = 17
        SOSPENSIONE_ALTRO = 18

        SOSPENSIONE_ANNULLATA = 19

        STRAGIUDIZIALE_AFFIDAMENTO_LEGALE = 20

        AGGIORNAMENTO_SITUAZIONE = 21

        MAV_ERRATO = 94
        MAV_ANNULLATO_RIGENERATO = 95
        RATEIZZAZIONE = 96
        DEBITO_VARIATO = 97
        MOROSITA_ANNULLATA = 98
        MAV_AGGIORNATO = 99
        MOROSITA_CONCLUSA = 100

        AGG_POSTALER_MANUALENTE = 101
        ANNULLA_RIGENERA_MAV = 102
        ANNULLA_RIGENERA_LETTERA = 103
        ANNULLA_MAV_LETTERA = 104

    End Enum

    '********MANUTENZIONI

    Public Class Manutenzioni_Interventi
        Private _id As Integer
        Private _Tipologia As String

        Private _IdImpianto As Integer
        Private _IdComplesso As Integer
        Private _idEdificio As Integer
        Private _idUnita As Integer
        Private _idUnitaComune As Integer
        Private _idScala As Integer
        Private _Dettaglio As String

        Private _Importo As String
        Private _ImportoC As String
        Private _ImportoR As String
        Private _fl_bloccato As Integer

        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property TIPOLOGIA() As String
            Get
                Return _Tipologia
            End Get
            Set(ByVal value As String)
                Me._Tipologia = value
            End Set
        End Property

        Public Property ID_IMPIANTO() As Integer
            Get
                Return _IdImpianto
            End Get
            Set(ByVal value As Integer)
                Me._IdImpianto = value
            End Set
        End Property

        Public Property ID_COMPLESSO() As Integer
            Get
                Return _IdComplesso
            End Get
            Set(ByVal value As Integer)
                Me._IdComplesso = value
            End Set
        End Property

        Public Property ID_EDIFICIO() As Integer
            Get
                Return _idEdificio
            End Get
            Set(ByVal value As Integer)
                Me._idEdificio = value
            End Set
        End Property

        Public Property ID_SCALA() As Integer
            Get
                Return _idScala
            End Get
            Set(ByVal value As Integer)
                Me._idScala = value
            End Set
        End Property

        Public Property ID_UNITA_IMMOBILIARE() As Integer
            Get
                Return _idUnita
            End Get
            Set(ByVal value As Integer)
                Me._idUnita = value
            End Set
        End Property

        Public Property ID_UNITA_COMUNE() As Integer
            Get
                Return _idUnitaComune
            End Get
            Set(ByVal value As Integer)
                Me._idUnitaComune = value
            End Set
        End Property

        Public Property DETTAGLIO() As String
            Get
                Return _Dettaglio
            End Get
            Set(ByVal value As String)
                Me._Dettaglio = value
            End Set
        End Property


        Public Property IMPORTO_PRESUNTO() As String
            Get
                Return _Importo
            End Get
            Set(ByVal value As String)
                Me._Importo = value
            End Set
        End Property

        Public Property IMPORTO_CONSUNTIVO() As String
            Get
                Return _ImportoC
            End Get
            Set(ByVal value As String)
                Me._ImportoC = value
            End Set
        End Property

        Public Property IMPORTO_RIMBORSO() As String
            Get
                Return _ImportoR
            End Get
            Set(ByVal value As String)
                Me._ImportoR = value
            End Set
        End Property


        Public Property FL_BLOCCATO() As Integer
            Get
                Return _fl_bloccato
            End Get
            Set(ByVal value As Integer)
                Me._fl_bloccato = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal tipologia As String, ByVal id_impianto As Integer, ByVal id_complesso As Integer, ByVal id_edificio As Integer, ByVal id_unita As Integer, ByVal id_comune As Integer, ByVal dettaglio As String, ByVal importo As String, ByVal importoC As String, ByVal importoR As String, ByVal fl_bloccato As Integer, ByVal id_scala As Integer)

            Me.ID = id
            Me.TIPOLOGIA = tipologia
            Me.ID_IMPIANTO = id_impianto
            Me.ID_COMPLESSO = id_complesso
            Me.ID_EDIFICIO = id_edificio
            Me.ID_UNITA_IMMOBILIARE = id_unita
            Me.ID_UNITA_COMUNE = id_comune
            Me.ID_SCALA = ID_SCALA

            Me.DETTAGLIO = dettaglio
            Me.IMPORTO_PRESUNTO = importo
            Me.IMPORTO_CONSUNTIVO = importoC
            Me.IMPORTO_RIMBORSO = importoR
            Me.FL_BLOCCATO = fl_bloccato

        End Sub
    End Class




    Public Class ListaGenerale
        Private _id As Integer
        Private _str As String

        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        Public Property STR() As String
            Get
                Return _str
            End Get
            Set(ByVal value As String)
                Me._str = value
            End Set
        End Property


        Public Sub New(ByVal id As Integer, ByVal str As String)

            Me.ID = id
            Me.STR = str

        End Sub
    End Class


    'IMPIANTI - CENTRALE TERMICA (lista UI per EDIFICIO ALIMENTATO)
    Public Class ListaUI
        Private _idEdificio As Integer
        Private _idUnita As Integer

        Public Property ID_EDIFICIO() As Integer
            Get
                Return _idEdificio
            End Get
            Set(ByVal value As Integer)
                Me._idEdificio = value
            End Set
        End Property

        Public Property ID_UNITA() As Integer
            Get
                Return _idUnita
            End Get
            Set(ByVal value As Integer)
                Me._idUnita = value
            End Set
        End Property


        Public Sub New(ByVal idEdificio As Integer, ByVal idUnita As Integer)

            Me.ID_EDIFICIO = idEdificio
            Me.ID_UNITA = idUnita

        End Sub
    End Class


    Public Class EdificiCT
        Private _id As Integer

        Private _Denominazione As String
        Private _TotUI_AL As Integer
        Private _TotUI As Integer
        Private _MQ_AL As Double
        Private _MQ As Double
        Private _Unita As String

        Private _chk As Boolean


        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property


        Public Property DENOMINAZIONE() As String
            Get
                Return _Denominazione
            End Get
            Set(ByVal value As String)
                Me._Denominazione = value
            End Set
        End Property


        Public Property TOTALE_UI_AL() As Integer
            Get
                Return _TotUI_AL
            End Get
            Set(ByVal value As Integer)
                Me._TotUI_AL = value
            End Set
        End Property

        Public Property TOTALE_UI() As Integer
            Get
                Return _TotUI
            End Get
            Set(ByVal value As Integer)
                Me._TotUI = value
            End Set
        End Property

        Public Property TOTALE_MQ_AL() As Double
            Get
                Return _MQ_AL
            End Get
            Set(ByVal value As Double)
                Me._MQ_AL = value
            End Set
        End Property

        Public Property TOTALE_MQ() As Double
            Get
                Return _MQ
            End Get
            Set(ByVal value As Double)
                Me._MQ = value
            End Set
        End Property

        Public Property UNITA() As String
            Get
                Return _Unita
            End Get
            Set(ByVal value As String)
                Me._Unita = value
            End Set
        End Property

        Public Property CHK() As Boolean
            Get
                Return _chk
            End Get
            Set(ByVal value As Boolean)
                Me._chk = value
            End Set
        End Property

        Public Sub New(ByVal id As Integer, ByVal denominazione As String, ByVal TotUI_AL As Integer, ByVal TotUI As Integer, ByVal MQ_AL As Double, ByVal MQ As Double, ByVal Unita As String, ByVal chk As Boolean)

            Me.ID = id
            Me.DENOMINAZIONE = denominazione
            Me.TOTALE_UI_AL = TotUI_AL
            Me.TOTALE_UI = TotUI
            Me.TOTALE_MQ_AL = MQ_AL
            Me.TOTALE_MQ = MQ
            Me.UNITA = Unita
            Me.CHK = chk

        End Sub

    End Class

End Class


