
Partial Class ANAUT_RicercaApplicazioneAU0
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Public Property Tipo11() As Integer
        Get
            If Not (ViewState("par_Tipo11") Is Nothing) Then
                Return CInt(ViewState("par_Tipo11"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo11") = value
        End Set
    End Property

    Public Property Tipo1() As Integer
        Get
            If Not (ViewState("par_Tipo1") Is Nothing) Then
                Return CInt(ViewState("par_Tipo1"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo1") = value
        End Set
    End Property

    Public Property Tipo2() As Integer
        Get
            If Not (ViewState("par_Tipo2") Is Nothing) Then
                Return CInt(ViewState("par_Tipo2"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo2") = value
        End Set
    End Property

    Public Property Tipo3() As Integer
        Get
            If Not (ViewState("par_Tipo3") Is Nothing) Then
                Return CInt(ViewState("par_Tipo3"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo3") = value
        End Set
    End Property

    Public Property Tipo4() As Integer
        Get
            If Not (ViewState("par_Tipo4") Is Nothing) Then
                Return CInt(ViewState("par_Tipo4"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo4") = value
        End Set
    End Property

    Public Property Tipo5() As Integer
        Get
            If Not (ViewState("par_Tipo5") Is Nothing) Then
                Return CInt(ViewState("par_Tipo5"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo5") = value
        End Set
    End Property

    Public Property Tipo6() As Integer
        Get
            If Not (ViewState("par_Tipo6") Is Nothing) Then
                Return CInt(ViewState("par_Tipo6"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo6") = value
        End Set
    End Property

    Public Property Tipo7() As Integer
        Get
            If Not (ViewState("par_Tipo7") Is Nothing) Then
                Return CInt(ViewState("par_Tipo7"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo7") = value
        End Set
    End Property

    Public Property Tipo8() As Integer
        Get
            If Not (ViewState("par_Tipo8") Is Nothing) Then
                Return CInt(ViewState("par_Tipo8"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo8") = value
        End Set
    End Property


    Public Property Tipo9() As Integer
        Get
            If Not (ViewState("par_Tipo9") Is Nothing) Then
                Return CInt(ViewState("par_Tipo9"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo9") = value
        End Set
    End Property

    Public Property Tipo10() As Integer
        Get
            If Not (ViewState("par_Tipo10") Is Nothing) Then
                Return CInt(ViewState("par_Tipo10"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Tipo10") = value
        End Set
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If Not IsPostBack Then
            CaricaDatiAU()
            CaricaStrutture()
            'CaricaTipoContratti()
            CaricaTipoContrattiNew()
            CaricaSindacati()
        End If

        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        txtSloggioDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtSloggioAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        txtPGDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtPGAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Function CaricaTipoContrattiNew()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                PAR.SettaCommand(PAR)
            End If

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI where id=" & cmbAU.SelectedItem.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                If PAR.IfNull(myReader("erp_1"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP SOCIALE", "1"))
                End If

                If PAR.IfNull(myReader("erp_2"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP MODERATO", "2"))
                End If

                If PAR.IfNull(myReader("erp_FF_OO"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP FF.OO.", "3"))
                End If

                If PAR.IfNull(myReader("erp_art_22"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP ART.22 C.10", "4"))
                End If

                If PAR.IfNull(myReader("erp_conv"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP CONVENZIONATO", "5"))
                End If

                If PAR.IfNull(myReader("ERP_4"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP art.15 comma 2-vizi amministrativi", "6"))
                End If

                If PAR.IfNull(myReader("ERP_3"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("ERP Contratti precari Art. 15 let. a, b", "7"))
                End If

                If PAR.IfNull(myReader("L39278"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("392/78", "8"))
                End If

                If PAR.IfNull(myReader("L43198"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("431/98", "9"))
                End If

                If PAR.IfNull(myReader("OA"), "") = "1" Then
                    chListRU.Items.Add(New ListItem("Occupazioni Abusive", "10"))
                End If

            End If
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Dim i As Integer = 0
            For i = 0 To chListRU.Items.Count - 1
                chListru.Items(i).Selected = True
            Next

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Function CaricaSindacati()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbSindacato.Items.Add(New ListItem("TUTTI", "0"))
            PAR.cmd.CommandText = "SELECT * FROM sindacati_vsa"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                cmbSindacato.Items.Add(New ListItem(PAR.IfNull(myReader("descrizione"), ""), myReader("id")))
            Loop

            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    'Private Function CaricaTipoContratti()
    '    Try
    '        If PAR.OracleConn.State = Data.ConnectionState.Closed Then
    '            PAR.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If
    '        cmbTipoContratto.Items.Clear()
    '        cmbTipoContratto.Items.Add(New ListItem("TUTTI", "0"))
    '        PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI where id=" & cmbAU.SelectedItem.Value
    '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
    '        If myReader.Read Then
    '            If PAR.IfNull(myReader("erp_1"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP SOCIALE", "1"))
    '            End If

    '            If PAR.IfNull(myReader("erp_2"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP MODERATO", "2"))
    '            End If

    '            If PAR.IfNull(myReader("erp_FF_OO"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP FF.OO.", "3"))
    '            End If

    '            If PAR.IfNull(myReader("erp_art_22"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP ART.22 C.10", "4"))
    '            End If

    '            If PAR.IfNull(myReader("erp_conv"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP CONVENZIONATO", "5"))
    '            End If

    '            If PAR.IfNull(myReader("ERP_4"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP art.15 comma 2-vizi amministrativi", "6"))
    '            End If

    '            If PAR.IfNull(myReader("ERP_3"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("ERP Contratti precari Art. 15 let. a, b", "7"))
    '            End If

    '            If PAR.IfNull(myReader("L39278"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("392/78", "8"))
    '            End If

    '            If PAR.IfNull(myReader("L43198"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("431/98", "9"))
    '            End If

    '            If PAR.IfNull(myReader("OA"), "") = "1" Then
    '                cmbTipoContratto.Items.Add(New ListItem("Occupazioni Abusive", "10"))
    '            End If

    '        End If
    '        myReader.Close()

    '        PAR.OracleConn.Close()
    '        PAR.cmd.Dispose()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        PAR.OracleConn.Close()
    '        PAR.cmd.Dispose()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '    End Try
    'End Function

    Private Function CaricaDatiAU()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI where stato=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                cmbAU.Items.Add(New ListItem(PAR.IfNull(myReader("descrizione"), ""), PAR.IfNull(myReader("id"), "0")))

                Tipo1 = PAR.IfNull(myReader("ERP_1"), 0) 'ERP SOCIALE
                Tipo2 = PAR.IfNull(myReader("ERP_2"), 0) 'ERP MODERATO
                Tipo3 = PAR.IfNull(myReader("ERP_ART_22"), 0) 'ART 200 C 10
                Tipo4 = PAR.IfNull(myReader("ERP_4"), 0)
                Tipo5 = PAR.IfNull(myReader("ERP_5"), 0)
                Tipo10 = PAR.IfNull(myReader("ERP_3"), 0)
                Tipo6 = PAR.IfNull(myReader("L43198"), 0)
                Tipo7 = PAR.IfNull(myReader("L39278"), 0)
                Tipo8 = PAR.IfNull(myReader("ERP_FF_OO"), 0) 'FF.OO.
                Tipo9 = PAR.IfNull(myReader("ERP_CONV"), 0) 'ERP CONVENZIONATO
                Tipo11 = PAR.IfNull(myReader("OA"), 0)
            Loop
            myReader.Close()



            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Function CaricaStrutture()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            chListStrutture.Items.Clear()
            PAR.cmd.CommandText = "SELECT UTENZA_FILIALI.*,TAB_FILIALI.NOME FROM SISCOM_MI.TAB_FILIALI,UTENZA_FILIALI WHERE utenza_filiali.fl_eliminato=0 and TAB_FILIALI.ID=UTENZA_filiali.ID_STRUTTURA AND ID_BANDO=" & cmbAU.SelectedItem.Value & " AND TUTTO_PATRIMONIO=0 order by nome asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            Do While myReader.Read
                chListStrutture.Items.Add(New ListItem(PAR.IfNull(myReader("NOME"), ""), PAR.IfNull(myReader("id"), "0")))
            Loop
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Private Function CaricaSportelli()
        Try
            Dim i As Integer = 0
            Dim s As String = ""

            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            chListSportelli.Items.Clear()

            For i = 0 To chListStrutture.Items.Count - 1
                If chListStrutture.Items(i).Selected = True Then
                    s = s & chListStrutture.Items(i).Value & ","
                End If
            Next
            If s <> "" Then
                s = Mid(s, 1, Len(s) - 1)
                s = " WHERE utenza_sportelli.fl_eliminato=0 and ID_FILIALE IN (" & s & ")"

                PAR.cmd.CommandText = "SELECT UTENZA_SPORTELLI.* FROM UTENZA_SPORTELLI " & s & " order by DESCRIZIONE asc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myReader.Read
                    chListSportelli.Items.Add(New ListItem(PAR.IfNull(myReader("DESCRIZIONE"), ""), PAR.IfNull(myReader("id"), "0")))
                Loop
                myReader.Close()

            End If



            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function

    Protected Sub cmbAU_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAU.SelectedIndexChanged
        CaricaStrutture()
        CaricaTipoContrattiNew()
    End Sub

    Protected Sub chListStrutture_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles chListStrutture.SelectedIndexChanged
        CaricaSportelli()
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim trovato As Boolean = False
        Dim b As Boolean = False
        Dim stringaSQL As String = ""
        Dim sStringaSql1 As String = ""


        For i = 0 To chListStrutture.Items.Count - 1
            If chListStrutture.Items(i).Selected = True Then
                trovato = True
            End If
        Next
        If trovato = False Then
            Response.Write("<script>alert('Selezionare almeno una struttura!');</script>")
            Exit Sub
        End If

        trovato = False
        For i = 0 To chListSportelli.Items.Count - 1
            If chListSportelli.Items(i).Selected = True Then
                trovato = True
            End If
        Next
        If trovato = False Then
            Response.Write("<script>alert('Selezionare almeno uno sportello/sede!');</script>")
            Exit Sub
        End If

        If cmbCompDa.SelectedItem.Text <> "--" And cmbCompA.SelectedItem.Text <> "" Then
            If CInt(cmbCompDa.SelectedItem.Text) > CInt(Replace(cmbCompA.SelectedItem.Text, "--", "100")) Then
                Response.Write("<script>alert('Intervallo numero componenti errato!');</script>")
                Exit Sub
            End If
        End If


        stringaSQL = " CONVOCAZIONI_AU.ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE ID_AU=" & cmbAU.SelectedItem.Value & ") "

        b = True

        Dim s As String = ""
        Dim strutture As String = ""

        For i = 0 To chListStrutture.Items.Count - 1
            If chListStrutture.Items(i).Selected = True Then
                s = s & chListStrutture.Items(i).Value & ","
                strutture = strutture & chListStrutture.Items(i).Text & ","
            End If
        Next
        If s <> "" Then
            s = Mid(s, 1, Len(s) - 1)
            If b = True Then
                s = " AND CONVOCAZIONI_AU.ID_FILIALE IN (SELECT ID_STRUTTURA FROM UTENZA_FILIALI WHERE ID IN (" & s & "))"
            Else
                s = " CONVOCAZIONI_AU.ID_FILIALE IN (SELECT ID_STRUTTURA FROM UTENZA_FILIALI WHERE ID IN (" & s & "))"
            End If
            b = True
        End If
        stringaSQL = stringaSQL & s


        s = ""
        Dim Sportelli As String = ""
        For i = 0 To chListSportelli.Items.Count - 1
            If chListSportelli.Items(i).Selected = True Then
                s = s & chListSportelli.Items(i).Value & ","
                Sportelli = Sportelli & chListSportelli.Items(i).Text & ","
            End If
        Next
        If s <> "" Then
            s = Mid(s, 1, Len(s) - 1)
            If b = True Then
                s = " AND CONVOCAZIONI_AU.ID_SPORTELLO IN (" & s & ")"
            Else
                s = " CONVOCAZIONI_AU.ID_SPORTELLO IN (" & s & ")"
            End If
            b = True
        End If

        stringaSQL = stringaSQL & s


        Dim NumComponenti As String = "Tutti"

        s = ""
        If cmbCompDa.SelectedItem.Text <> "--" Or cmbCompA.SelectedItem.Text <> "--" Then
            If cmbCompDa.SelectedItem.Text <> "--" And cmbCompA.SelectedItem.Text = "--" Then
                If b = True Then
                    s = " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO>=" & cmbCompDa.SelectedItem.Text
                Else
                    s = " UTENZA_DICHIARAZIONI.N_COMP_NUCLEO>=" & cmbCompDa.SelectedItem.Text
                End If
                NumComponenti = "da " & cmbCompDa.SelectedItem.Text
            End If
            If cmbCompDa.SelectedItem.Text = "--" And cmbCompA.SelectedItem.Text <> "--" Then
                If b = True Then
                    s = " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO<=" & cmbCompA.SelectedItem.Text
                Else
                    s = " UTENZA_DICHIARAZIONI.N_COMP_NUCLEO<=" & cmbCompA.SelectedItem.Text
                End If
                NumComponenti = "fino a " & cmbCompA.SelectedItem.Text
            End If
            If cmbCompDa.SelectedItem.Text <> "--" And cmbCompA.SelectedItem.Text <> "--" Then
                If b = True Then
                    s = " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO>=" & cmbCompDa.SelectedItem.Text & " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO<=" & cmbCompA.SelectedItem.Text
                Else
                    s = " UTENZA_DICHIARAZIONI.N_COMP_NUCLEO>=" & cmbCompDa.SelectedItem.Text & " AND UTENZA_DICHIARAZIONI.N_COMP_NUCLEO<=" & cmbCompA.SelectedItem.Text
                End If
                NumComponenti = "Da " & cmbCompDa.SelectedItem.Text & " fino a " & cmbCompA.SelectedItem.Text
            End If

            b = True
        End If
        stringaSQL = stringaSQL & s


        s = ""
        Dim NumComponenti65 As String = "Indifferente"

        If cmb65.SelectedItem.Text = "SI" Or cmb65.SelectedItem.Text = "NO" Then
            If cmb65.SelectedItem.Text = "SI" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.PRESENZA_MAG_65,0)>0"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.PRESENZA_MAG_65,0)>0"
                End If
                NumComponenti65 = "SI"
            End If

            If cmb65.SelectedItem.Text = "NO" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.PRESENZA_MAG_65,0)=0"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.PRESENZA_MAG_65,0)=0"
                End If
                NumComponenti65 = "NO"
            End If
            b = True

        End If
        stringaSQL = stringaSQL & s


        s = ""
        Dim NumComponenti15 As String = "Indifferente"

        If cmb15.SelectedItem.Text = "SI" Or cmb15.SelectedItem.Text = "NO" Then
            If cmb15.SelectedItem.Text = "SI" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.PRESENZA_MIN_15,0)>0"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.PRESENZA_MIN_15,0)>0"
                End If
                NumComponenti15 = "SI"
            End If

            If cmb15.SelectedItem.Text = "NO" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.PRESENZA_MIN_15,0)=0"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.PRESENZA_MIN_15,0)=0"
                End If
                NumComponenti15 = "NO"
            End If
            b = True

        End If
        stringaSQL = stringaSQL & s

        s = ""
        Dim NumComponenti6699 As String = "Indifferente"

        If cmb6699.SelectedItem.Text = "SI" Or cmb6699.SelectedItem.Text = "NO" Then
            If cmb6699.SelectedItem.Text = "SI" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_66,0)>0"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_66,0)>0"
                End If
                NumComponenti6699 = "SI"
            End If

            If cmb6699.SelectedItem.Text = "NO" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_66,0)=0"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_66,0)=0"
                End If
                NumComponenti6699 = "NO"
            End If
            b = True

        End If
        stringaSQL = stringaSQL & s

        s = ""
        Dim NumComponenti100Non As String = "Indifferente"

        If cmb100Non.SelectedItem.Text = "SI" Or cmb100Non.SelectedItem.Text = "NO" Then
            If cmb100Non.SelectedItem.Text = "SI" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_SENZA,0)<>'0'"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_SENZA,0)<>'0'"
                End If
                NumComponenti100Non = "SI"
            End If

            If cmb100Non.SelectedItem.Text = "NO" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_SENZA,0)='0'"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_SENZA,0)='0'"
                End If
                NumComponenti100Non = "NO"
            End If
            b = True

        End If
        stringaSQL = stringaSQL & s

        s = ""
        Dim StatoAU As String = ""

        Select Case cmbStatoAU.SelectedItem.Value
            Case "0"
                StatoAU = "Stato Dich. AU: TUTTI"
            Case "1"
                StatoAU = "Stato Dich. AU: COMPLETE"
                If b = True Then
                    s = " AND UTENZA_DICHIARAZIONI.ID_STATO=1 "
                Else
                    s = " UTENZA_DICHIARAZIONI.ID_STATO=1 "
                End If
                b = True
            Case "2"
                StatoAU = "Stato Dich. AU: DA COMPLETARE"
                If b = True Then
                    s = " AND UTENZA_DICHIARAZIONI.ID_STATO=0 "
                Else
                    s = " UTENZA_DICHIARAZIONI.ID_STATO=0 "
                End If
                b = True
            Case "3"
                StatoAU = "Stato Dich. AU: SOSP. PER DOC. MANCANTE"
                If b = True Then
                    s = " AND UTENZA_DICHIARAZIONI.FL_SOSP_7='1' "
                Else
                    s = " UTENZA_DICHIARAZIONI.FL_SOSP_7='1' "
                End If
                b = True
        End Select
        stringaSQL = stringaSQL & s

        s = ""
        Dim NumComponenti100Acc As String = "Indifferente"

        If cmb100Acc.SelectedItem.Text = "SI" Or cmb100Acc.SelectedItem.Text = "NO" Then
            If cmb100Acc.SelectedItem.Text = "SI" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_CON,0)<>'0'"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_CON,0)<>'0'"
                End If
                NumComponenti100Acc = "SI"
            End If

            If cmb100Acc.SelectedItem.Text = "NO" Then
                If b = True Then
                    s = " AND NVL(UTENZA_DICHIARAZIONI.N_INV_100_CON,0)='0'"
                Else
                    s = " NVL(UTENZA_DICHIARAZIONI.N_INV_100_CON,0)='0'"
                End If
                NumComponenti100Acc = "NO"
            End If
            b = True

        End If
        stringaSQL = stringaSQL & s


        s = ""
        Dim RedditoDip As String = "Indifferente"

        If cmbDip.SelectedItem.Text = "SI" Or cmbDip.SelectedItem.Text = "NO" Then
            If cmbDip.SelectedItem.Text = "SI" Then
                If b = True Then
                    s = " AND (SELECT NVL(SUM(IMPORTO),0) FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID))<>0 "
                Else
                    s = " (SELECT NVL(SUM(IMPORTO),0) FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID))<>0 "
                End If
                RedditoDip = "SI"
            End If

            If cmbDip.SelectedItem.Text = "NO" Then
                If b = True Then
                    s = " AND (SELECT NVL(SUM(IMPORTO),0) FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID))=0 "
                Else
                    s = " (SELECT NVL(SUM(IMPORTO),0) FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID))=0 "
                End If
                RedditoDip = "NO"
            End If
            b = True

        End If
        stringaSQL = stringaSQL & s

        s = ""
        Dim RedditoImm As String = "Indifferente"

        If cmbImmobiliare.SelectedItem.Text = "SI" Or cmbImmobiliare.SelectedItem.Text = "NO" Then
            If cmbImmobiliare.SelectedItem.Text = "SI" Then
                If b = True Then
                    s = " AND (SELECT NVL(SUM(VALORE),0) FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID)) <>0"
                Else
                    s = " (SELECT NVL(SUM(VALORE),0) FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID)) <>0"
                End If
                RedditoImm = "SI"
            End If

            If cmbImmobiliare.SelectedItem.Text = "NO" Then
                If b = True Then
                    s = " AND (SELECT NVL(SUM(VALORE),0) FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID)) =0"
                Else
                    s = " (SELECT NVL(SUM(VALORE),0) FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE IN (SELECT ID FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID)) =0"
                End If
                RedditoImm = "NO"
            End If
            b = True
        End If
        stringaSQL = stringaSQL & s

        s = ""
        Dim TUTORESTR As String = "Indifferente"
        If cmbTutore.SelectedItem.Text = "SI" Or cmbTutore.SelectedItem.Text = "NO" Then
            If cmbTutore.SelectedItem.Text = "SI" Then
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =1 "
                    TUTORESTR = "SI"
                Else
                    s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =1 "
                    TUTORESTR = "NO"
                End If
            End If
            If cmbTutore.SelectedItem.Text = "NO" Then
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =0 "
                    TUTORESTR = "SI"
                Else
                    s = " AND RAPPORTI_UTENZA.FL_TUTORE_STR =0 "
                    TUTORESTR = "NO"
                End If
            End If
        End If
        stringaSQL = stringaSQL & s


        s = ""
        Dim SINDACATO As String = "Indifferente"
        If cmbSindacato.SelectedItem.Text <> "TUTTI" Then
            If b = True Then
                s = " AND RAPPORTI_UTENZA.SINDACATO = " & cmbSindacato.SelectedItem.Value
                SINDACATO = cmbSindacato.SelectedItem.Text
            Else
                s = " RAPPORTI_UTENZA.SINDACATO =" & cmbSindacato.SelectedItem.Value
                SINDACATO = cmbSindacato.SelectedItem.Text
            End If

        End If
        stringaSQL = stringaSQL & s


        s = ""
        Dim stipula As String = ""
        If txtStipulaDal.Text <> "" Then
            stipula = "Dal " & txtStipulaDal.Text
            If b = True Then
                s = " AND RAPPORTI_UTENZA.data_stipula> = '" & PAR.AggiustaData(txtStipulaDal.Text) & "' "
            Else
                s = " RAPPORTI_UTENZA.data_stipula> = '" & PAR.AggiustaData(txtStipulaDal.Text) & "' "
            End If
        End If
        stringaSQL = stringaSQL & s


        s = ""
        If txtStipulaAl.Text <> "" Then
            stipula = stipula & " fino al " & txtStipulaAl.Text
            If b = True Then
                s = " AND RAPPORTI_UTENZA.data_stipula< = '" & PAR.AggiustaData(txtStipulaAl.Text) & "' "
            Else
                s = " RAPPORTI_UTENZA.data_stipula< = '" & PAR.AggiustaData(txtStipulaAl.Text) & "' "
            End If
        End If
        stringaSQL = stringaSQL & s


        If stipula = "" Then stipula = "Indifferente"


        s = ""
        Dim protocollo As String = ""
        If txtPGDal.Text <> "" Then
            protocollo = "Dal " & txtPGDal.Text
            If b = True Then
                s = " AND UTENZA_DICHIARAZIONI.DATA_PG> = '" & PAR.AggiustaData(txtPGDal.Text) & "' "
            Else
                s = " UTENZA_DICHIARAZIONI.DATA_PG> = '" & PAR.AggiustaData(txtPGDal.Text) & "' "
            End If
        End If
        stringaSQL = stringaSQL & s


        s = ""
        If txtPGAl.Text <> "" Then
            protocollo = protocollo & " fino al " & txtPGAl.Text
            If b = True Then
                s = " AND UTENZA_DICHIARAZIONI.DATA_PG< = '" & PAR.AggiustaData(txtPGAl.Text) & "' "
            Else
                s = " UTENZA_DICHIARAZIONI.DATA_PG< = '" & PAR.AggiustaData(txtPGAl.Text) & "' "
            End If
        End If
        stringaSQL = stringaSQL & s


        If protocollo = "" Then protocollo = "Indifferente"

        s = ""
        Dim sloggio As String = ""
        If txtSloggioDal.Text <> "" Then
            sloggio = "Dal " & txtSloggioDal.Text
            If b = True Then
                s = " AND RAPPORTI_UTENZA.data_riconsegna> = '" & PAR.AggiustaData(txtSloggioDal.Text) & "' "
            Else
                s = " RAPPORTI_UTENZA.data_riconsegna> = '" & PAR.AggiustaData(txtSloggioDal.Text) & "' "
            End If
        End If
        stringaSQL = stringaSQL & s

        s = ""
        If ElencoRU.Text <> "" Then
            If b = True Then
                s = " AND RAPPORTI_UTENZA.COD_CONTRATTO IN ('" & Replace(ElencoRU.Text, ",", "','") & "') "
            Else
                s = " RAPPORTI_UTENZA.COD_CONTRATTO IN ('" & Replace(ElencoRU.Text, ",", "','") & "') "
            End If
        End If
        stringaSQL = stringaSQL & s

        s = ""
        If txtSloggioAl.Text <> "" Then
            sloggio = sloggio & " fino al " & txtSloggioAl.Text
            If b = True Then
                s = " AND RAPPORTI_UTENZA.data_riconsegna< = '" & PAR.AggiustaData(txtSloggioAl.Text) & "' "
            Else
                s = " RAPPORTI_UTENZA.data_riconsegna< = '" & PAR.AggiustaData(txtSloggioAl.Text) & "' "
            End If
        End If
        stringaSQL = stringaSQL & s

        s = ""
        Dim DaVerificare As String = "NO"
        If chDaVerificare.Checked = True Then
            If b = True Then
                s = " AND nvl(fl_verifica_reddito,0)=0 and nvl(fl_verifica_nucleo,0)=0 and nvl(fl_verifica_patrimonio,0)=0 "
                DaVerificare = ""
            Else
                s = " nvl(fl_verifica_reddito,0)=0 and nvl(fl_verifica_nucleo,0)=0 and nvl(fl_verifica_patrimonio,0)=0 "
                DaVerificare = ""
            End If

        End If
        stringaSQL = stringaSQL & s

        If sloggio = "" Then sloggio = "Indifferente"

        s = "("
        Dim tipoC As String = ""
        Dim tipologiaC As String = ""
        For i = 0 To chListRU.Items.Count - 1
            If chListRU.Items(i).Selected = True Then
                's = s & chListRU.Items(i).Value & ","
                tipologiaC = tipologiaC & chListRU.Items(i).Text & ","
                Select Case chListRU.Items(i).Value
                    Case "1"
                        s = s & " (rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2) or "
                    Case "2"
                        s = s & " unita_immobiliari.id_destinazione_uso = 2 or "
                    Case "3"
                        's = s & " unita_immobiliari.id_destinazione_uso = 10 or "
                        s = s & " rapporti_utenza.provenienza_ass = 10 or "
                    Case "4"
                        s = s & " rapporti_utenza.provenienza_ass = 8 or "
                    Case "5"
                        s = s & " unita_immobiliari.id_destinazione_uso = 12 or "
                    Case "6"

                    Case "7"
                        s = s & " (rapporti_utenza.dest_uso = 'D' and rapporti_utenza.provenienza_ass <> 6) OR "
                    Case "8"

                    Case "9"

                    Case "10"
                        s = s & " rapporti_utenza.provenienza_ass = 7 or "
                End Select
            End If
        Next
        If b = True Then s = " and " & s
        If s = s & " (" Then
            s = " (rapporti_utenza.dest_uso='XXX') "
        Else
            s = Mid(s, 1, Len(s) - 4) & ")  "
        End If

        stringaSQL = stringaSQL & s

        s = ""
        Dim dt As New Data.DataTable
        dt = CType(HttpContext.Current.Session.Item("ElencoOriginaleDT"), Data.DataTable)

        's = ""
        'dt = CType(HttpContext.Current.Session.Item("ElencoRegistroDT"), Data.DataTable)
        If Not IsNothing(dt) Then
            For Each r As Data.DataRow In dt.Rows
                s = s & r.Item("IDC") & ","
            Next
            If s <> "" Then
                If b = True Then
                    s = " AND RAPPORTI_UTENZA.ID NOT IN (" & Mid(s, 1, Len(s) - 1) & ")"
                Else
                    s = " RAPPORTI_UTENZA.ID NOT IN (" & Mid(s, 1, Len(s) - 1) & ")"
                End If
            End If
        End If


        stringaSQL = stringaSQL & s & " and "



        'sStringaSql1 = "SELECT (CASE WHEN UTENZA_DICHIARAZIONI.ID_STATO=0 THEN 'DA COMPLETARE' WHEN UTENZA_DICHIARAZIONI.ID_STATO=1 THEN 'COMPLETA' WHEN UTENZA_DICHIARAZIONI.ID_STATO=2 THEN 'DA CANCELLARE' END) AS STATO_AU,NVL(EDIFICI.SCONTO_COSTO_BASE,-1000) AS SCONTO_COSTO_BASE,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO,UTENZA_SPORTELLI.DESCRIZIONE AS SPORTELLO,convocazioni_au.*,RAPPORTI_UTENZA.COD_CONTRATTO,UTENZA_DICHIARAZIONI.PATR_SUPERATO,UTENZA_DICHIARAZIONI.PG,UTENZA_DICHIARAZIONI.ISEE,UTENZA_DICHIARAZIONI.ID_STATO,UTENZA_DICHIARAZIONI.id as idAU,UTENZA_DICHIARAZIONI.FL_SOSP_1,UTENZA_DICHIARAZIONI.FL_SOSP_2,UTENZA_DICHIARAZIONI.FL_SOSP_3,UTENZA_DICHIARAZIONI.FL_SOSP_4,UTENZA_DICHIARAZIONI.FL_SOSP_5,UTENZA_DICHIARAZIONI.FL_SOSP_7,unita_immobiliari.ID AS idunita,(SELECT (CASE WHEN TIPO=0 THEN 'INCOMPLETA' ELSE 'NON RISPONDENTE' END) FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & cmbAU.SelectedItem.Value & ") AS DIFFIDA,(SELECT TO_CHAR(TO_DATE(DATA_GENERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=CONVOCAZIONI_AU.ID_CONTRATTO AND ID_AU=" & cmbAU.SelectedItem.Value & ") AS DATA_GENERAZIONE_DIFFIDA FROM siscom_mi.convocazioni_au,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza,UTENZA_DICHIARAZIONI,UTENZA_SPORTELLI,SISCOM_MI.EDIFICI WHERE " & stringaSQL & "  UTENZA_DICHIARAZIONI.ID_STATO<>2 AND UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE=0 AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND rapporti_utenza.cod_contratto=UTENZA_DICHIARAZIONI.rapporto  AND UTENZA_DICHIARAZIONI.id_bando=" & cmbAU.SelectedItem.Value & " AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND NVL(FL_GENERAZ_AUTO,0)=0 AND rapporti_utenza.ID=convocazioni_au.id_contratto AND unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto=convocazioni_au.id_contratto AND unita_immobiliari.ID=unita_contrattuale.id_unita AND convocazioni_au.id_contratto IS NOT NULL AND data_app IS NOT NULL AND id_gruppo IN (SELECT ID FROM siscom_mi.convocazioni_au_gruppi WHERE id_au=" & cmbAU.SelectedItem.Value & ") "


        sStringaSql1 = "SELECT " _
                     & " UTENZA_DICHIARAZIONI.ID AS IDAU," _
                     & " UTENZA_DICHIARAZIONI.pg AS PG_AU," _
                     & " UTENZA_DICHIARAZIONI.N_COMP_NUCLEO," _
                     & " UTENZA_DICHIARAZIONI.N_INV_100_CON," _
                     & " UTENZA_DICHIARAZIONI.N_INV_100_SENZA," _
                     & " UTENZA_DICHIARAZIONI.N_INV_100_66 AS n_inv_66_99," _
                     & " DECODE(PREVALENTE_DIP,0,'NO',1,'SI') AS PREVALENTE," _
                     & " DECODE(PRESENZA_MIN_15,0,'NO',1,'SI') AS PRESENZA_15," _
                     & " DECODE(PRESENZA_MAG_65,0,'NO',1,'SI') AS PRESENZA_65, " _
                     & " RAPPORTI_UTENZA.ID AS IDC," _
                     & " COD_CONTRATTO," _
                     & " ANAGRAFICA.COGNOME," _
                     & " ANAGRAFICA.NOME," _
                     & " COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA," _
                     & " TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA," _
                     & " TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA," _
                     & " INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA," _
                     & " INDIRIZZI.CIVICO AS CIVICO_UNITA," _
                     & " INDIRIZZI.CAP AS CAP_UNITA," _
                     & " INDIRIZZI.LOCALITA AS COMUNE_UNITA," _
                     & " UTENZA_SPORTELLI.DESCRIZIONE AS FILIALE " _
                     & " FROM " _
                     & " siscom_mi.unita_contrattuale, " _
                     & " SISCOM_MI.ANAGRAFICA, " _
                     & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                     & " SISCOM_MI.RAPPORTI_UTENZA, " _
                     & " SISCOM_MI.INDIRIZZI, " _
                     & " SISCOM_MI.UNITA_IMMOBILIARI," _
                     & " UTENZA_DICHIARAZIONI," _
                     & " siscom_mi.convocazioni_au," _
                     & " UTENZA_SPORTELLI" _
                     & " WHERE " _
                     & stringaSQL _
                     & " UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND " _
                     & " rapporti_utenza.ID=convocazioni_au.id_contratto AND convocazioni_au.id_contratto IS NOT NULL AND data_app IS NOT NULL AND id_gruppo IN (SELECT ID FROM siscom_mi.convocazioni_au_gruppi WHERE id_au=" & cmbAU.SelectedItem.Value & ")" _
                     & " AND NVL(FL_GENERAZ_AUTO,0)=0 AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA<>'VEND' AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND ((UTENZA_DICHIARAZIONI.FL_SOSP_7=1 AND UTENZA_DICHIARAZIONI.FL_SOSP_1=0 AND UTENZA_DICHIARAZIONI.FL_SOSP_2=0 AND UTENZA_DICHIARAZIONI.FL_SOSP_3=0 AND UTENZA_DICHIARAZIONI.FL_SOSP_4=0 AND UTENZA_DICHIARAZIONI.FL_SOSP_5=0) OR (UTENZA_DICHIARAZIONI.FL_SOSP_7 = 0 AND UTENZA_DICHIARAZIONI.FL_SOSP_1 = 0 AND UTENZA_DICHIARAZIONI.FL_SOSP_2 = 0 AND UTENZA_DICHIARAZIONI.FL_SOSP_3 = 0 AND UTENZA_DICHIARAZIONI.FL_SOSP_4 = 0 AND UTENZA_DICHIARAZIONI.FL_SOSP_5 = 0))  " _
                     & " AND UTENZA_DICHIARAZIONI.ID_BANDO=" & cmbAU.SelectedItem.Value & " AND " _
                     & " UTENZA_DICHIARAZIONI.RAPPORTO IS NOT NULL AND " _
                     & " UTENZA_DICHIARAZIONI.RAPPORTO=RAPPORTI_UTENZA.COD_CONTRATTO AND " _
                     & " UTENZA_DICHIARAZIONI.ID NOT IN (SELECT ID_DICHIARAZIONE FROM UTENZA_GRUPPI_DICHIARAZIONI WHERE ID_DICHIARAZIONE IS NOT NULL AND ID_GRUPPO IN (SELECT ID FROM UTENZA_GRUPPI_LAVORO WHERE ID_BANDO_AU=" & cmbAU.SelectedItem.Value & ")) AND " _
                     & " (anagrafica.ragione_sociale IS NULL OR anagrafica.ragione_sociale='') AND  " _
                     & " ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND " _
                     & " SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
                     & " SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND " _
                     & " INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND " _
                     & " UNITA_IMMOBILIARI.ID=unita_contrattuale.id_unita AND " _
                     & " unita_contrattuale.id_contratto=rapporti_utenza.ID AND " _
                     & " unita_contrattuale.id_unita_principale IS NULL  AND  " _
                     & " COD_CONTRATTO IS NOT NULL AND CONVOCAZIONI_AU.ID_STATO=2 "


        sStringaSql1 = sStringaSql1 & " ORDER BY indirizzi.descrizione asc,indirizzi.civico asc,anagrafica.cognome ASC,anagrafica.nome asc"
            Session.Add("PGAPPLICAZIONEAU", sStringaSql1)
            Response.Redirect("RisultatoApplicazioneAU.aspx")

        'Response.Write("<script>window.open('SimulaGenerale.aspx?Q=" & PAR.Cripta("STRUTTURE:" & strutture & "-SPORTELLI:" & Sportelli & "-Num.Comp.:" & NumComponenti & "-Maggiori 65:" & NumComponenti65 & "-minori 15:" & NumComponenti15 & "-Comp.Inv. 66-99%:" & NumComponenti6699 & "-Comp.Inv.100% No ACC.:" & NumComponenti100Non & "-Comp.Inv.100% Acc.:" & NumComponenti100Acc & "-Redd.Prev.Dip.:" & RedditoDip & "-Redd.Immob.:" & RedditoImm & "-Tutore Str.:" & TUTORESTR & "-Sindacato Rif.:" & SINDACATO & "-Stipula:" & stipula & "-Sloggio:" & sloggio & "-Tipo Rapporto:" & tipologiaC) & "&TIPOC=" & tipoC & "&IDB=" & cmbAU.SelectedItem.Value & "&S=" & PAR.Cripta(stringaSQL) & "&ST=" & StatoAU & "','','');</script>")
    End Sub
End Class
