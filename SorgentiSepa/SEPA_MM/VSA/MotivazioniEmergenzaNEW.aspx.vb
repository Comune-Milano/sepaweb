
Partial Class VSA_MotivazioniEmergenzaNEW
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            iddomanda.Value = Request.QueryString("ID")
            iddichiarazione.Value = Request.QueryString("DI")
            strConness = Request.QueryString("CONN")
            CaricaCategorie()
            CaricaInfo()

            Dim CTRL As Control
            For Each CTRL In form1.Controls
                If TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';")
                ElseIf TypeOf CTRL Is CheckBox Then

                    Select Case DirectCast(CTRL, CheckBox).ID
                        Case "chkAbilitaAI"
                            DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';ControlliAbilita('rdbAI','" & DirectCast(CTRL, CheckBox).ID & "');")
                        Case "chkAbilitaAA"
                            DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';ControlliAbilita('rdbAA','" & DirectCast(CTRL, CheckBox).ID & "');")
                        Case "chkAbilitaH"
                            DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';ControlliAbilita('rdbH','" & DirectCast(CTRL, CheckBox).ID & "');")
                        Case "chkAbilitaCD"
                            DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';ControlliAbilita('rdbCD','" & DirectCast(CTRL, CheckBox).ID & "');")
                        Case "chkAbilitaFS"
                            DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';ControlliAbilita('rdbFS','" & DirectCast(CTRL, CheckBox).ID & "');")
                        Case "chkAbilitaAN"
                            DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';ControlliAbilita('rdbAN','" & DirectCast(CTRL, CheckBox).ID & "');")
                        Case Else
                            DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';")

                    End Select
                End If
            Next

        


        End If

        For Each li As ListItem In rdbCD.Items
            li.Attributes.Add("onclick", "javascript:vediDiv('" + li.Value + "');")
        Next

    End Sub

    Private Sub ElencoCategorie()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(strConness), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & strConness), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT * FROM TAB_PUNTI_EMERGENZE ORDER BY ID ASC"
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            da.Fill(dt)
            da.Dispose()

            Dim i As Integer = 1
            Dim k As Integer = 0
            Dim strRadioButton As String = ""
            Dim gruppo As String = ""
            Dim strTabella As String = ""
            Dim id As Integer = 0

            lblCat.Text = "<table width='100%' cellpadding='2' cellspacing='0' border='1px'>"
            lblCat.Text &= "<tr style='font-family: arial, Helvetica, sans-serif; font-size: 10pt;'><td width='120px' align='center'><b>CATEGORIA</b></td><td align='center'><b>MOTIVAZIONI</b></td></tr>"

            Dim strFine As String = ""
            Dim cod As String = ""
            Do While k < dt.Rows.Count - 1
                If k > 0 Then
                    If par.IfNull(dt.Rows(k).Item("COD"), "") <> par.IfNull(dt.Rows(k + 1).Item("COD"), "") Then
                        cod = par.IfNull(dt.Rows(k + 1).Item("COD"), "")
                    Else
                        cod = ""
                    End If
                Else
                    cod = par.IfNull(dt.Rows(k).Item("COD"), "")
                End If

                If cod <> "" Then
                    lblCat.Text &= "<tr style='font-family: arial;'><td width='80px'>" & i & ") <b>" & cod & "</b></td>"

                    par.cmd.CommandText = "SELECT * FROM TAB_PUNTI_EMERGENZE where COD ='" & cod & "'"
                    Dim da2 As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dt2 As New Data.DataTable
                    da2 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    da2.Fill(dt2)
                    da2.Dispose()

                    If dt2.Rows.Count > 0 Then
                        strRadioButton = "<td style='font-family: arial; font-size: 9pt;'>"
                        For Each row As Data.DataRow In dt2.Rows
                            id = par.IfNull(row.Item("ID"), "")
                            gruppo = cod
                            If cod = "CD" Then
                                strRadioButton &= "<input id=Check" & id & " name='" & cod & "' type='checkbox' value='" & id & "'/>" & par.IfNull(row.Item("DESCRIZIONE"), "") & "<br />"
                            Else
                                strRadioButton &= "<input id=Radio" & id & " name='" & gruppo & "' type='radio' value='" & id & "'/>" & par.IfNull(row.Item("DESCRIZIONE"), "") & "<br />"
                            End If
                        Next
                        strRadioButton &= "</td>"
                    End If
                    lblCat.Text &= strRadioButton & "</tr>"
                    i = i + 1
                End If
                k = k + 1
            Loop

            lblCat.Text &= "</table>"

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaCategorie()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(strConness), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & strConness), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT * FROM TAB_PUNTI_EMERGENZE ORDER BY ID ASC"
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            da.Fill(dt)
            da.Dispose()

            Dim k As Integer = 0
            Dim cod As String = ""
            Do While k < dt.Rows.Count - 1
                If k > 0 Then
                    If par.IfNull(dt.Rows(k).Item("COD"), "") <> par.IfNull(dt.Rows(k + 1).Item("COD"), "") Then
                        cod = par.IfNull(dt.Rows(k + 1).Item("COD"), "")
                    Else
                        cod = ""
                    End If
                Else
                    cod = par.IfNull(dt.Rows(k).Item("COD"), "")
                End If

                If cod <> "" Then

                    par.cmd.CommandText = "SELECT * FROM TAB_PUNTI_EMERGENZE where COD ='" & cod & "'"
                    Dim da2 As Oracle.DataAccess.Client.OracleDataAdapter
                    Dim dt2 As New Data.DataTable
                    da2 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                    da2.Fill(dt2)
                    da2.Dispose()

                    If dt2.Rows.Count > 0 Then
                        For Each row As Data.DataRow In dt2.Rows
                            Select Case row.Item("COD")
                                Case "AI"
                                    rdbAI.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "EA"
                                    chkEA.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "AA"
                                    rdbAA.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "IV"
                                    chkIV.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "H"
                                    rdbH.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "AN"
                                    rdbAN.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "CD"
                                    If par.IfNull(row.Item("ID"), 0) = 12 Then
                                        chkCD.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                    Else
                                        rdbCD.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                    End If
                                Case "FS"
                                    rdbFS.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "PV"
                                    chkPV.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "BA"
                                    chkBA.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "ACC"
                                    chkACC.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "AN+"
                                    chkANN.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "H+"
                                    chkHH.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                                Case "NE"
                                    chkNE.Items.Add(New ListItem(par.IfNull(row.Item("DESCRIZIONE"), ""), par.IfNull(row.Item("ID"), 0)))
                            End Select
                        Next
                    End If
                End If
                k = k + 1
            Loop

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaInfo()

        par.OracleConn = CType(HttpContext.Current.Session.Item(strConness), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & strConness), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Dim trovato As Integer = 0
        par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA_PUNTI_EM,TAB_PUNTI_EMERGENZE WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=TAB_PUNTI_EMERGENZE.ID AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dt As New Data.DataTable
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt)
        da.Dispose()
        For Each row As Data.DataRow In dt.Rows
            Select Case row.Item("COD")
                Case "AI"
                    rdbAI.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "EA"
                    chkEA.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "AA"
                    rdbAA.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "IV"
                    chkIV.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "H"
                    rdbH.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "AN"
                    rdbAN.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "CD"
                    If par.IfNull(row.Item("ID"), 0) = 12 Then
                        chkCD.SelectedValue = par.IfNull(row.Item("ID"), 0)
                    Else
                        rdbCD.SelectedValue = par.IfNull(row.Item("ID"), 0)
                    End If
                Case "FS"
                    rdbFS.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "PV"
                    chkPV.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "BA"
                    chkBA.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "ACC"
                    chkACC.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "AN+"
                    chkANN.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "H+"
                    chkHH.SelectedValue = par.IfNull(row.Item("ID"), 0)
                Case "NE"
                    chkNE.SelectedValue = par.IfNull(row.Item("ID"), 0)
            End Select

            trovato = 1

        Next

        If trovato = 0 Then

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE PERC_INVAL>=66 AND ID_DICHIARAZIONE=" & iddichiarazione.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                NumInvalidi = NumInvalidi + 1

                If myReader("perc_inval") >= 66 And myReader("perc_inval") <= 99 Then
                    NumInvalidi66_99 = NumInvalidi66_99 + 1

                    rdbH.Items(2).Selected = True
                End If

                If myReader("perc_inval") = 100 And par.IfNull(myReader("indennita_acc"), "0") = "1" Then
                    NumInvalidi100_i = NumInvalidi100_i + 1
                    rdbH.Items(0).Selected = True
                End If

                If myReader("perc_inval") = 100 And par.IfNull(myReader("indennita_acc"), "0") = "0" Then
                    NumInvalidi100 = NumInvalidi100 + 1
                    rdbH.Items(1).Selected = True
                End If

                'rdbH.Enabled = False
            Loop
            myReader.Close()


            NumComponenti = 0
            NumAnziani = 0

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & iddichiarazione.Value
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                NumComponenti = NumComponenti + 1
                If par.RicavaEta(par.IfNull(myReader("DATA_NASCITA"), "")) >= 65 And par.RicavaEta(par.IfNull(myReader("DATA_NASCITA"), "")) < 75 Then
                    'rdbAN.Enabled = False
                    rdbAN.Items(0).Selected = True

                    NumAnziani = NumAnziani + 1
                End If
                If par.RicavaEta(par.IfNull(myReader("DATA_NASCITA"), "")) >= 75 Then
                    'rdbAN.Enabled = False
                    rdbAN.Items(1).Selected = True

                    NumAnziani = NumAnziani + 1
                End If
            Loop
            myReader.Close()

            If NumAnziani > 1 Then
                chkANN.Items(0).Selected = True
                'chkANN.Enabled = False
            End If


            Dim sup As Double = 0
            Dim ascensore As String = "0"
            Dim piano_a As String = "0"
            Dim codUI As String = ""

            par.cmd.CommandText = "SELECT * FROM domande_VSA_alloggio WHERE ID_domanda=" & iddomanda.Value
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                sup = par.IfNull(myReader("sup_netta"), 0)
                piano_a = par.IfNull(myReader("piano"), 0)
                ascensore = par.IfNull(myReader("ascensore"), 0)
                codUI = par.IfNull(myReader("cod_unita_immobiliare"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM alloggi WHERE COD_ALLOGGIO = '" & codUI & "' AND ID_pratica=" & iddomanda.Value
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("BARRIERE_ARC"), "") = "0" Then
                    barrArch = "0"
                Else
                    barrArch = "1"
                End If
            Else
                barrArch = "-1"
            End If
            myReader.Close()

            If NumInvalidi > 0 And barrArch = "1" Then
                chkBA.Items(0).Selected = True
            End If

            If NumInvalidi > 1 Then
                chkHH.Items(0).Selected = True
            End If

            If NumAnziani > 0 Or NumInvalidi > 0 Then
                If ascensore = "0" And piano_a > 2 Then
                    chkACC.Items(0).Selected = True
                    'chkACC.Enabled = False
                Else
                    chkACC.Items(0).Selected = False
                End If
            End If

            If NumInvalidi > 0 And barrArch = False Then
                For i = 0 To rdbH.Items.Count - 1
                    If rdbH.Items(i).Selected = True Then
                        rdbH.Items(i).Selected = False
                        rdbH.Enabled = True
                    End If
                Next
            End If


            Select Case NumComponenti
                Case 1
                    If sup >= 72.46 Then
                        rdbCD.Items(0).Selected = True
                    End If
                    If sup >= 55.66 Then
                        rdbCD.Items(1).Selected = True
                    End If
                Case 2
                    If sup >= 83.35 Then
                        rdbCD.Items(0).Selected = True
                    End If
                    If sup >= 66.55 Then
                        rdbCD.Items(1).Selected = True
                    End If
                Case 3
                    If sup >= 95.5 Then
                        rdbCD.Items(0).Selected = True
                    End If
                    If sup >= 78.65 Then
                        rdbCD.Items(1).Selected = True
                    End If
                Case 4
                    If sup >= 113.6 Then
                        rdbCD.Items(0).Selected = True
                    End If
                    If sup >= 96.8 Then
                        rdbCD.Items(1).Selected = True
                    End If
            End Select

            Select Case NumComponenti
                Case 0, 1, 2
                    'rdbFS.Enabled = False
                Case 3
                    If sup <= 16.8 Then
                        rdbFS.Items(0).Selected = True
                    Else
                        rdbFS.Items(0).Selected = False
                    End If
                Case 4, 5
                    If sup <= 33.6 Then
                        rdbFS.Items(1).Selected = True
                    Else
                        rdbFS.Items(1).Selected = False
                    End If
                Case 6
                    If sup <= 50.4 Then
                        rdbFS.Items(2).Selected = True
                    Else
                        rdbFS.Items(2).Selected = False
                    End If
                Case 7
                    If sup <= 67.2 Then
                        rdbFS.Items(3).Selected = True
                    Else
                        rdbFS.Items(3).Selected = False
                    End If
                Case Is >= 8
                    If sup <= 84 Then
                        rdbFS.Items(4).Selected = True
                    Else
                        rdbFS.Items(4).Selected = False
                    End If
            End Select



            If Request.QueryString("MOD") = "1" Then
                Response.Write("<script>alert('Attenzione, salvare la domanda prima di inserire le motivazioni di cambio alloggio!');</script>")
                ImgSalva.Visible = False
            Else
                If sup = "0" Then
                    Response.Write("<script>alert('Attenzione, la superficie dell\'unità non è stata caricata. Assecurarsi di aver inserito tale superficie e di aver premuto il pulsante SALVA!');</script>")
                    ImgSalva.Visible = False
                End If
            End If



        End If

        ControllaAbilitazioni()

    End Sub

    Protected Sub ControllaAbilitazioni()

        chkAbilitaAI.Checked = False
        chkAbilitaAA.Checked = False
        chkAbilitaAN.Checked = False
        chkAbilitaH.Checked = False
        chkAbilitaCD.Checked = False
        chkAbilitaFS.Checked = False

        For i = 0 To rdbAI.Items.Count - 1
            If rdbAI.Items(i).Selected = True Then
                chkAbilitaAI.Checked = True
                Exit For
            End If

        Next

        For i = 0 To rdbAA.Items.Count - 1
            If rdbAA.Items(i).Selected = True Then
                chkAbilitaAA.Checked = True
                Exit For
            End If
        Next

        For i = 0 To rdbH.Items.Count - 1
            If rdbH.Items(i).Selected = True Then
                chkAbilitaH.Checked = True
                Exit For
            End If
        Next

        For i = 0 To rdbAN.Items.Count - 1
            If rdbAN.Items(i).Selected = True Then
                chkAbilitaAN.Checked = True
                Exit For
            End If
        Next

        For i = 0 To rdbCD.Items.Count - 1
            If rdbCD.Items(i).Selected = True Then
                chkAbilitaCD.Checked = True
                Exit For
            End If
        Next

        For i = 0 To rdbFS.Items.Count - 1
            If rdbFS.Items(i).Selected = True Then
                chkAbilitaFS.Checked = True
                Exit For
            End If
        Next



    End Sub

    Public Property barrArch() As Integer
        Get
            If Not (ViewState("par_barrArch") Is Nothing) Then
                Return CInt(ViewState("par_barrArch"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_barrArch") = value
        End Set

    End Property

    Public Property strConness() As String
        Get
            If Not (ViewState("par_strConness") Is Nothing) Then
                Return CStr(ViewState("par_strConness"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_strConness") = value
        End Set
    End Property

    Public Property NumAnziani() As Long
        Get
            If Not (ViewState("par_NumAnziani") Is Nothing) Then
                Return CLng(ViewState("par_NumAnziani"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_NumAnziani") = value
        End Set

    End Property

    Public Property NumInvalidi100() As Long
        Get
            If Not (ViewState("par_NumInvalidi100") Is Nothing) Then
                Return CLng(ViewState("par_NumInvalidi100"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_NumInvalidi100") = value
        End Set

    End Property

    Public Property NumInvalidi100_i() As Long
        Get
            If Not (ViewState("par_NumInvalidi100_i") Is Nothing) Then
                Return CLng(ViewState("par_NumInvalidi100_i"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_NumInvalidi100_i") = value
        End Set

    End Property

    Public Property NumInvalidi66_99() As Long
        Get
            If Not (ViewState("par_NumInvalidi66_99") Is Nothing) Then
                Return CLng(ViewState("par_NumInvalidi66_99"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_NumInvalidi66_99") = value
        End Set

    End Property

    Public Property NumInvalidi() As Long
        Get
            If Not (ViewState("par_NumInvalidi") Is Nothing) Then
                Return CLng(ViewState("par_NumInvalidi"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_NumInvalidi") = value
        End Set

    End Property

    Public Property NumComponenti() As Long
        Get
            If Not (ViewState("par_NumComponenti") Is Nothing) Then
                Return CLng(ViewState("par_NumComponenti"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_NumComponenti") = value
        End Set

    End Property

    Protected Sub ImgSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(strConness), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & strConness), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim inserPunteggio As Integer = 0
            Dim AA As Boolean = False
            Dim AI As Boolean = False
            Dim EA As Boolean = False




            If chkAbilitaAI.Checked = True And pieno(rdbAI) = "1" Then

                For i = 0 To rdbAI.Items.Count - 1
                    If rdbAI.Items(i).Selected = True Then
                        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbAI.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderD.Read Then
                            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbAI.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & rdbAI.SelectedValue & ")"
                            inserPunteggio = 1

                        End If
                        myReaderD.Close()
                        AI = True
                        par.cmd.ExecuteNonQuery()
                    End If
                Next
            Else
                For i = 0 To rdbAI.Items.Count - 1
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbAI.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbAI.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()

                    End If
                    myReaderD.Close()
                Next


            End If



            For i = 0 To chkEA.Items.Count - 1
                If chkEA.Items(i).Selected = True Then
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkEA.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbEA.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                    Else
                        par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkEA.SelectedValue & ")"
                        inserPunteggio = 1

                    End If
                    myReaderD.Close()
                    EA = True
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkEA.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkEA.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderD.Close()
                End If
            Next




            If chkAbilitaAA.Checked = True And pieno(rdbAA) = "1" Then
                For i = 0 To rdbAA.Items.Count - 1
                    If rdbAA.Items(i).Selected = True Then
                        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbAA.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderD.Read Then
                            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbAA.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & rdbAA.SelectedValue & ")"
                            inserPunteggio = 1

                        End If
                        myReaderD.Close()
                        AA = True
                        par.cmd.ExecuteNonQuery()
                    End If
                Next

            Else
                For i = 0 To rdbAA.Items.Count - 1

                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbAA.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbAA.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()

                    End If
                    myReaderD.Close()
                Next
            End If




            For i = 0 To chkIV.Items.Count - 1
                If chkIV.Items(i).Selected = True Then
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkIV.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbIV.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                    Else
                        par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkIV.SelectedValue & ")"
                        inserPunteggio = 1
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkIV.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkIV.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderD.Close()
                End If
            Next






            If chkAbilitaH.Checked = True And pieno(rdbH) = "1" Then

                For i = 0 To rdbH.Items.Count - 1
                    If rdbH.Items(i).Selected = True Then
                        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbH.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderD.Read Then
                            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbH.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & rdbH.SelectedValue & ")"
                            inserPunteggio = 1
                        End If
                        myReaderD.Close()
                        par.cmd.ExecuteNonQuery()
                    End If
                Next

            Else

                For i = 0 To rdbH.Items.Count - 1

                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbH.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbH.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()

                    End If
                    myReaderD.Close()
                Next

            End If







            'For i = 0 To rdbAN.Items.Count - 1
            '    If rdbAN.Items(i).Selected = True Then
            '        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbAN.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
            '        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '        If myReaderD.Read Then
            '            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbAN.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
            '        Else
            '            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & rdbAN.SelectedValue & ")"
            '            inserPunteggio = 1
            '        End If
            '        par.cmd.ExecuteNonQuery()
            '    End If
            'Next



            'For i = 0 To rdbAN.Items.Count - 1
            '    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbAN.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
            '    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReaderD.Read Then
            '        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbAN.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
            '        par.cmd.ExecuteNonQuery()

            '    End If
            'Next

            'For i = 0 To rdbAN.Items.Count - 1
            '    If rdbAN.Items(i).Selected = True Then
            '        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbAN.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
            '        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '        If myReaderD.Read Then
            '            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbAN.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
            '        Else
            '            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & rdbAN.SelectedValue & ")"
            '            inserPunteggio = 1
            '        End If
            '        par.cmd.ExecuteNonQuery()
            '    End If
            'Next


            If chkAbilitaAN.Checked = True And pieno(rdbAN) = "1" Then

                For i = 0 To rdbAN.Items.Count - 1
                    If rdbAN.Items(i).Selected = True Then
                        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbAN.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderD.Read Then
                            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbH.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & rdbAN.SelectedValue & ")"
                            inserPunteggio = 1
                        End If
                        myReaderD.Close()
                        par.cmd.ExecuteNonQuery()
                    End If
                Next

            Else

                For i = 0 To rdbAN.Items.Count - 1

                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbAN.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbAN.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()

                    End If
                    myReaderD.Close()
                Next

            End If














            If chkAbilitaCD.Checked = True And pieno(rdbCD) = "1" Then

                For i = 0 To rdbCD.Items.Count - 1
                    If rdbCD.Items(i).Selected = True Then
                        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbCD.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderD.Read Then
                            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & chkCD.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & rdbCD.SelectedValue & ")"
                            inserPunteggio = 1
                        End If
                        myReaderD.Close()
                        par.cmd.ExecuteNonQuery()
                    Else
                        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbCD.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderD.Read Then
                            par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbCD.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                            par.cmd.ExecuteNonQuery()
                        End If
                        myReaderD.Close()
                    End If
                Next

            Else

                For i = 0 To rdbCD.Items.Count - 1

                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbCD.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbCD.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()

                    End If
                    myReaderD.Close()
                Next

            End If


            For i = 0 To chkCD.Items.Count - 1
                If chkCD.Items(i).Selected = True Then
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkCD.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & chkCD.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                    Else
                        par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkCD.SelectedValue & ")"
                        inserPunteggio = 1
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkCD.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkCD.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderD.Close()
                End If
            Next





            If chkAbilitaFS.Checked = True And pieno(rdbFS) = "1" Then

                For i = 0 To rdbFS.Items.Count - 1
                    If rdbFS.Items(i).Selected = True Then
                        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbFS.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderD.Read Then
                            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbFS.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                        Else
                            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & rdbFS.SelectedValue & ")"
                            inserPunteggio = 1
                        End If
                        myReaderD.Close()
                        par.cmd.ExecuteNonQuery()
                    End If
                Next

            Else

                For i = 0 To rdbFS.Items.Count - 1

                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & rdbFS.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbFS.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()

                    End If
                    myReaderD.Close()
                Next


            End If





            For i = 0 To chkPV.Items.Count - 1
                If chkPV.Items(i).Selected = True Then
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkPV.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbPV.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                    Else
                        par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkPV.SelectedValue & ")"
                        inserPunteggio = 1
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkPV.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkPV.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderD.Close()
                End If
            Next
            'For i = 0 To chkCM.Items.Count - 1
            '    If chkCM.Items(i).Selected = True Then
            '        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkCM.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
            '        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '        If myReaderD.Read Then
            '            'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbCM.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
            '        Else
            '            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkCM.SelectedValue & ")"
            '            inserPunteggio = 1
            '        End If
            '        par.cmd.ExecuteNonQuery()
            '    Else
            '        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkCM.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
            '        Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '        If myReaderD.Read Then
            '            par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkCM.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
            '            par.cmd.ExecuteNonQuery()
            '        End If
            '    End If
            'Next
            For i = 0 To chkBA.Items.Count - 1
                If chkBA.Items(i).Selected = True Then
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkBA.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbBA.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                    Else
                        par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkBA.SelectedValue & ")"
                        inserPunteggio = 1
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkBA.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkBA.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderD.Close()
                End If
            Next
            For i = 0 To chkACC.Items.Count - 1
                If chkACC.Items(i).Selected = True Then
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkACC.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbACC.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                    Else
                        par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkACC.SelectedValue & ")"
                        inserPunteggio = 1
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkACC.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkACC.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            For i = 0 To chkANN.Items.Count - 1
                If chkANN.Items(i).Selected = True Then
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkANN.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & chkANN.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                    Else
                        If NumAnziani > 1 Then
                            For ann As Integer = 0 To NumAnziani - 1
                                par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkANN.SelectedValue & ")"
                            Next
                        End If
                        inserPunteggio = 1
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkANN.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkANN.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            For i = 0 To chkHH.Items.Count - 1
                If chkHH.Items(i).Selected = True Then
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkHH.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & chkHH.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                    Else
                        If NumInvalidi > 1 Then
                            For hh As Integer = 0 To NumInvalidi - 1
                                par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkHH.SelectedValue & ")"
                            Next
                        End If
                        inserPunteggio = 1
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkHH.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkHH.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            For i = 0 To chkNE.Items.Count - 1
                If chkNE.Items(i).Selected = True Then
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkNE.SelectedValue & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        'par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_PUNTI_EM SET ID_PUNTEGGIO=" & rdbNE.SelectedValue & " WHERE ID_DOMANDA=" & iddomanda.Value
                    Else
                        par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & "," & chkNE.SelectedValue & ")"
                        inserPunteggio = 1
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=" & chkNE.Items(i).Value & " AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkNE.Items(i).Value & " AND ID_DOMANDA=" & iddomanda.Value
                    End If
                    myReaderD.Close()
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            'End If
            'myReader1.Close()


            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_PUNTI_EM.* FROM DOMANDE_BANDO_VSA_PUNTI_EM,TAB_PUNTI_EMERGENZE WHERE DOMANDE_BANDO_VSA_PUNTI_EM.ID_PUNTEGGIO=TAB_PUNTI_EMERGENZE.ID AND DOMANDE_BANDO_VSA_PUNTI_EM.ID_DOMANDA=" & iddomanda.Value
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.HasRows = True Then
                If myReader1.Read = True Then

                End If
            Else
                If inserPunteggio = 0 Then
                    par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_PUNTI_EM (ID_DOMANDA,ID_PUNTEGGIO) VALUES (" & iddomanda.Value & ",26)"
                    par.cmd.ExecuteNonQuery()
                End If
            End If
            myReader1.Close()

            If (AI = True And AA = True) Or (AI = True And EA = True) Or (AA = True And EA = True) Then
                Response.Write("<script>alert('Attenzione...i punteggi delle categorie AI, EA e AA non sono tra loro cumulabili! Salvataggio non effettuato!')</script>")
                If AI = True Then
                    par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbAI.SelectedValue & " AND ID_DOMANDA=" & iddomanda.Value
                    par.cmd.ExecuteNonQuery()
                End If
                If AA = True Then
                    par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & rdbAA.SelectedValue & " AND ID_DOMANDA=" & iddomanda.Value
                    par.cmd.ExecuteNonQuery()
                End If
                If EA = True Then
                    par.cmd.CommandText = "DELETE FROM DOMANDE_BANDO_VSA_PUNTI_EM WHERE ID_PUNTEGGIO=" & chkEA.SelectedValue & " AND ID_DOMANDA=" & iddomanda.Value
                    par.cmd.ExecuteNonQuery()
                End If
                rdbAA.SelectedIndex = "-1"
                rdbAI.SelectedIndex = "-1"
                chkEA.SelectedIndex = "-1"
                Exit Sub
            End If

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & strConness, par.myTrans)

            Modificato.Value = "0"
            Response.Write("<script>alert('Operazione Effettuata! PREMERE IL PULSANTE SALVA NELLA DOMANDA PER RENDERE EFFETTIVE LE MODIFICHE!');</script>")

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Function pieno(ByVal rlb As RadioButtonList) As String

        pieno = "0"
        For i = 0 To rlb.Items.Count - 1
            If rlb.Items(i).Selected = True Then
                pieno = "1"
            End If
        Next

    End Function



End Class
