
Partial Class VSA_MotivazioniEmergenza
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Private Function Valore01(ByVal valore As Boolean) As Integer
        If valore = True Then
            Valore01 = 1
        Else
            Valore01 = 0
        End If
    End Function


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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Label1.Text = "MOTIVAZIONI CAMBIO IN EMERGENZA (ART.22 C.10 RR 1/2004)"
            HyperLink1.NavigateUrl = "../Contratti/DatiComplessoEdificio.aspx?COD=" & Request.QueryString("IDE")
            HyperLink1.Target = "_blank"

            iddomanda.Value = Request.QueryString("ID")
            iddichiarazione.Value = Request.QueryString("DI")
            strConness = Request.QueryString("CONN")
            If Request.QueryString("X") = "1" Then
                ImgSalva.Visible = False

                CheckBox1.Enabled = False
                CheckBox2.Enabled = False
                CheckBox3.Enabled = False
                CheckBox4.Enabled = False
                CheckBox5.Enabled = False
                CheckBox6.Enabled = False
                CheckBox7.Enabled = False
                CheckBox8.Enabled = False
                CheckBox9.Enabled = False
                CheckBox10.Enabled = False
                CheckBox11.Enabled = False
                CheckBox12.Enabled = False
                CheckBox13.Enabled = False
                CheckBox14.Enabled = False
                CheckBox15.Enabled = False
                CheckBox16.Enabled = False

                CheckBox17.Enabled = False
                CheckBox18.Enabled = False
                CheckBox19.Enabled = False
                CheckBox20.Enabled = False
                CheckBox21.Enabled = False
                CheckBox12.Enabled = False
                CheckBox22.Enabled = False
                TXTaLTRO.Enabled = False


                
            End If
            Carica()
        End If

        Dim CTRL As Control
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('Modificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('Modificato').value='1';")
            End If
        Next


    End Sub

    Private Function Carica()
        par.OracleConn = CType(HttpContext.Current.Session.Item(strConness), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & strConness), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA_MOT_CAMBI.* FROM DOMANDE_BANDO_VSA_MOT_CAMBI WHERE DOMANDE_BANDO_VSA_MOT_CAMBI.ID_DOMANDA=" & iddomanda.Value
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.HasRows = True Then
            If myReader1.Read = True Then
                If par.IfNull(myReader1("V1"), 0) = 0 Then
                    CheckBox1.Checked = False
                Else
                    CheckBox1.Checked = True
                End If

                If par.IfNull(myReader1("V2"), 0) = 0 Then
                    CheckBox2.Checked = False
                Else
                    CheckBox2.Checked = True
                End If

                If par.IfNull(myReader1("V3"), 0) = 0 Then
                    CheckBox3.Checked = False
                Else
                    CheckBox3.Checked = True
                End If

                If par.IfNull(myReader1("V4"), 0) = 0 Then
                    CheckBox4.Checked = False
                Else
                    CheckBox4.Checked = True
                End If

                If par.IfNull(myReader1("V5"), 0) = 0 Then
                    CheckBox5.Checked = False
                Else
                    CheckBox5.Checked = True
                End If

                If par.IfNull(myReader1("V6"), 0) = 0 Then
                    CheckBox6.Checked = False
                Else
                    CheckBox6.Checked = True
                End If

                If par.IfNull(myReader1("V7"), 0) = 0 Then
                    CheckBox7.Checked = False
                Else
                    CheckBox7.Checked = True
                End If

                If par.IfNull(myReader1("V8"), 0) = 0 Then
                    CheckBox8.Checked = False
                Else
                    CheckBox8.Checked = True
                End If

                If par.IfNull(myReader1("V9"), 0) = 0 Then
                    CheckBox9.Checked = False
                Else
                    CheckBox9.Checked = True
                End If

                If par.IfNull(myReader1("V10"), 0) = 0 Then
                    CheckBox10.Checked = False
                Else
                    CheckBox10.Checked = True
                End If

                If par.IfNull(myReader1("V11"), 0) = 0 Then
                    CheckBox11.Checked = False
                Else
                    CheckBox11.Checked = True
                End If

                If par.IfNull(myReader1("V12"), 0) = 0 Then
                    CheckBox12.Checked = False
                Else
                    CheckBox12.Checked = True
                End If

                If par.IfNull(myReader1("V13"), 0) = 0 Then
                    CheckBox13.Checked = False
                Else
                    CheckBox13.Checked = True
                End If

                If par.IfNull(myReader1("V14"), 0) = 0 Then
                    CheckBox14.Checked = False
                Else
                    CheckBox14.Checked = True
                End If

                If par.IfNull(myReader1("V15"), 0) = 0 Then
                    CheckBox15.Checked = False
                Else
                    CheckBox15.Checked = True
                End If

                If par.IfNull(myReader1("V16"), 0) = 0 Then
                    CheckBox16.Checked = False
                Else
                    CheckBox16.Checked = True
                End If

                If par.IfNull(myReader1("V17"), 0) = 0 Then
                    CheckBox17.Checked = False
                Else
                    CheckBox17.Checked = True
                End If

                If par.IfNull(myReader1("V18"), 0) = 0 Then
                    CheckBox18.Checked = False
                Else
                    CheckBox18.Checked = True
                End If

                If par.IfNull(myReader1("V19"), 0) = 0 Then
                    CheckBox19.Checked = False
                Else
                    CheckBox19.Checked = True
                End If

                If par.IfNull(myReader1("V20"), 0) = 0 Then
                    CheckBox20.Checked = False
                Else
                    CheckBox20.Checked = True
                End If

                If par.IfNull(myReader1("V21"), 0) = 0 Then
                    CheckBox21.Checked = False
                Else
                    CheckBox21.Checked = True
                End If

                If par.IfNull(myReader1("V22"), 0) = 0 Then
                    CheckBox22.Checked = False
                Else
                    CheckBox22.Checked = True
                End If

                TXTaLTRO.Text = par.IfNull(myReader1("ALTRO"), "")

            End If
        Else
            par.cmd.CommandText = "INSERT INTO DOMANDE_BANDO_VSA_MOT_CAMBI (ID_DOMANDA) VALUES (" & iddomanda.Value & ")"
            par.cmd.ExecuteNonQuery()
        End If
        myReader1.Close()

        CheckBox14.Checked = False
        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE PERC_INVAL>=66 AND ID_DICHIARAZIONE=" & iddichiarazione.Value
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReader.Read
            NumInvalidi = NumInvalidi + 1

            If myReader("perc_inval") >= 66 And myReader("perc_inval") <= 99 Then
                NumInvalidi66_99 = NumInvalidi66_99 + 1
                'CheckBox25.Checked = True
            Else
                'CheckBox25.Checked = False
            End If

            If myReader("perc_inval") = 100 And par.IfNull(myReader("indennita_acc"), "0") = "1" Then
                NumInvalidi100_i = NumInvalidi100_i + 1
                'CheckBox23.Checked = True
            Else
                'CheckBox23.Checked = False
            End If

            If myReader("perc_inval") = 100 And par.IfNull(myReader("indennita_acc"), "0") = "0" Then
                NumInvalidi100 = NumInvalidi100 + 1
                'CheckBox24.Checked = True
            Else
                'CheckBox24.Checked = False
            End If

            CheckBox14.Checked = True
        Loop
        myReader.Close()

        If NumInvalidi100_i > 0 Then
            CheckBox23.Checked = True
        Else
            CheckBox23.Checked = False
            If NumInvalidi100 > 0 Then
                CheckBox24.Checked = True
            Else
                CheckBox24.Checked = False
                If NumInvalidi66_99 > 0 Then
                    CheckBox25.Checked = True
                Else
                    CheckBox25.Checked = False
                End If
            End If
        End If


        CheckBox14.Enabled = False


        NumComponenti = 0
        NumAnziani = 0

        CheckBox13.Checked = False
        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA WHERE ID_DICHIARAZIONE=" & iddichiarazione.Value
        myReader = par.cmd.ExecuteReader()
        Do While myReader.Read
            NumComponenti = NumComponenti + 1
            If par.RicavaEta(par.IfNull(myReader("DATA_NASCITA"), "")) >= 65 Then
                CheckBox13.Checked = True
                NumAnziani = NumAnziani + 1
            End If
        Loop
        myReader.Close()
        CheckBox13.Enabled = False

        If CheckBox13.Checked = False And CheckBox14.Checked = False Then
            CheckBox17.Enabled = False
            CheckBox18.Enabled = False
            CheckBox19.Enabled = False
            CheckBox20.Enabled = False
            CheckBox21.Enabled = False
            CheckBox12.Enabled = False
            CheckBox22.Enabled = False
            CheckBox8.Enabled = False

            CheckBox17.Checked = False
            CheckBox18.Checked = False
            CheckBox19.Checked = False
            CheckBox20.Checked = False
            CheckBox21.Checked = False
            CheckBox12.Checked = False

            CheckBox8.Checked = False
            CheckBox22.Checked = False

        End If

        Dim sup As Double = 0
        Dim ascensore As String = "0"
        Dim piano_a As String = "0"

        par.cmd.CommandText = "SELECT * FROM domande_VSA_alloggio WHERE ID_domanda=" & iddomanda.Value
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            sup = par.IfNull(myReader("sup_netta"), 0)
            piano_a = par.IfNull(myReader("piano"), 0)
            ascensore = par.IfNull(myReader("ascensore"), 0)
        End If
        myReader.Close()

        Select Case CInt(piano_a)
            Case 1, 2
                lblpiano.Text = "(PIANO INFERIORE AL SECONDO)"
            Case 32 To 43
                lblpiano.Text = "(PIANO INFERIORE AL SECONDO)"
        End Select


        If ascensore = "1" Then
            CheckBox17.Checked = False
            CheckBox17.Enabled = False
        End If

        lblSuperficie.Text = "N.Comp=" & NumComponenti & " - Sup.Alloggio=" & sup

        Select Case NumComponenti
            Case 0, 1, 2
                CheckBox16.Enabled = False
                CheckBox16.Checked = False
            Case 3
                If sup <= 16.8 Then
                    CheckBox16.Enabled = False
                    CheckBox16.Checked = True
                Else
                    CheckBox16.Enabled = False
                    CheckBox16.Checked = False
                End If
            Case 4, 5
                If sup <= 33.6 Then
                    CheckBox16.Enabled = False
                    CheckBox16.Checked = True
                Else
                    CheckBox16.Enabled = False
                    CheckBox16.Checked = False
                End If
            Case 6
                If sup <= 50.4 Then
                    CheckBox16.Enabled = False
                    CheckBox16.Checked = True
                Else
                    CheckBox16.Enabled = False
                    CheckBox16.Checked = False
                End If
            Case Is >= 7
                If sup <= 67.2 Then
                    CheckBox16.Enabled = False
                    CheckBox16.Checked = True
                Else
                    CheckBox16.Enabled = False
                    CheckBox16.Checked = False
                End If



        End Select

        If sup = "0" Then
            Response.Write("<script>alert('Attenzione, la superficie dell\'unità non è stata caricata. Assecurarsi di aver inserito tale superficie e di aver premuto il pulsante SALVA!');</script>")
            ImgSalva.Visible = False
        End If


    End Function

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

    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item(strConness), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & strConness), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        Dim SS As String = ""

        'OK
        If CheckBox1.Checked = True Then
            SS = SS & " AA=1,V1=1,"
        Else
            SS = SS & "AA=0,V1=0,"
        End If

        'OK
        If CheckBox2.Checked = True And CheckBox3.Checked = True Then
            SS = SS & " AI=1,V2=1,V3=1, "
        Else
            SS = SS & "AI=0,V2=0, "
            If CheckBox3.Checked = True Then
                SS = SS & "V3=0, "
            End If
        End If

        'OK 
        If CheckBox4.Checked = True Then
            SS = SS & " CD=1,V4=1, "
        Else
            SS = SS & "CD=0,V4=0, "
        End If

        'OK
        If CheckBox5.Checked = True Then
            SS = SS & " VC=1,V5=1, "
        Else
            SS = SS & "VC=0,V5=0, "
        End If

        'OK
        If CheckBox6.Checked = True Then
            SS = SS & " IV=1,V6=1, "
        Else
            SS = SS & "IV=0,V6=0, "
        End If

        'OK
        If CheckBox7.Checked = True Then
            SS = SS & " AE=1,V7=1, "
        Else
            SS = SS & "AE=0,V7=0, "
        End If

        'OK
        If CheckBox9.Checked = True Then
            SS = SS & " PV=1,V9=1, "
        Else
            SS = SS & "PV=0,V9=0, "
        End If


        'OK
        If CheckBox10.Checked = True Or CheckBox12.Checked = True Then
            SS = SS & " RI=1, "
        Else
            SS = SS & "RI=0, "
        End If
        If CheckBox10.Checked = True Then
            SS = SS & " V10=1, "
        Else
            SS = SS & "V10=0, "
        End If
        If CheckBox12.Checked = True Then
            SS = SS & " V12=1, "
        Else
            SS = SS & "V12=0, "
        End If

        'OK
        If CheckBox11.Checked = True Then
            SS = SS & " RU=1,V11=1, "
        Else
            SS = SS & "RU=0,V11=0, "
        End If

        'OK
        If CheckBox13.Checked = True Then
            SS = SS & " AN=1,V13=1, "
        Else
            SS = SS & "AN=0,V13=0, "
        End If

        'OK
        If CheckBox15.Checked = True Then
            SS = SS & " HM=1,V15=1, "
        Else
            SS = SS & "HM=0,V15=0, "
        End If

        'OK
        If CheckBox16.Checked = True Then
            SS = SS & " FS=1,V16=1, "
        Else
            SS = SS & "FS=0,V16=0, "
        End If

        If CheckBox23.Checked = True Then
            SS = SS & " HA=1, "
        Else
            SS = SS & "HA=0, "
        End If

        If CheckBox24.Checked = True Then
            SS = SS & " HT=1, "
        Else
            SS = SS & "HT=0, "
        End If

        If CheckBox25.Checked = True Then
            SS = SS & " HP=1, "
        Else
            SS = SS & "HP=0, "
        End If


        par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.DATA_presentazione FROM DOMANDE_BANDO_VSA WHERE ID=" & iddomanda.Value
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read Then
            SS = SS & " DATA_RICHIESTA='" & par.IfNull(myReader1("DATA_presentazione"), "") & "', "
        End If
        myReader1.Close()

        If NumInvalidi = True Then
            SS = SS & " HANDICAP=1, "
        Else
            SS = SS & "HANDICAP=0, "
        End If

        SS = SS & " N_ANZIANI_NUCLEO=" & NumAnziani & " , "

        par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA_MOT_CAMBI SET " & SS _
        & "V8=" & Valore01(CheckBox8.Checked) _
        & ",V14=" & Valore01(CheckBox14.Checked) _
        & ",V17=" & Valore01(CheckBox17.Checked) _
        & ",V18=" & Valore01(CheckBox18.Checked) _
        & ",V19=" & Valore01(CheckBox19.Checked) _
        & ",V20=" & Valore01(CheckBox20.Checked) _
        & ",V21=" & Valore01(CheckBox21.Checked) _
        & ",V22=" & Valore01(CheckBox22.Checked) _
        & ",ALTRO='" & par.PulisciStrSql(TXTaLTRO.Text) & "' WHERE ID_DOMANDA=" & iddomanda.Value
        par.cmd.ExecuteNonQuery()

        par.myTrans.Commit()
        par.myTrans = par.OracleConn.BeginTransaction()
        HttpContext.Current.Session.Add("TRANSAZIONE" & strConness, par.myTrans)

        Modificato.Value = "0"
        Response.Write("<script>alert('Operazione Effettuata! PREMERE IL PULSANTE SALVA NELLA DOMANDA PER RENDERE EFFETTIVE LE MODIFICHE!');</script>")

    End Sub
End Class
