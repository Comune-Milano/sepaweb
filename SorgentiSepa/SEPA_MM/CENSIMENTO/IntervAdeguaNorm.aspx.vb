
Partial Class CENSIMENTO_IntervAdeguaNorm
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim EsistDimens As Boolean = False
    Dim ValoreCaricato As String

 


    Private Sub salva()
        Try
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader
            Dim vIdAdegNorm As String = ""
            Dim idGestore As String
            idGestore = Mid(vId, 1, 1)

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Select Case Passato

                Case "ED"
                    '*************INSERT**********
                    par.cmd.CommandText = ""
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_NTERV_ADEG_NORM.NEXTVAL FROM DUAL"
                    myreader2 = par.cmd.ExecuteReader
                    If myreader2.Read Then
                        vIdAdegNorm = idGestore & myreader2(0)
                    End If
                    myreader2.Close()
                    If Me.TxtDescr.Text <> "" Then
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERV_ADEG_NORM (ID, ID_EDIFICIO,COD_TIPOLOGIA,DESCRIZIONE) VALUES (" & vIdAdegNorm & ", " & vId & ", '" & Me.DrlAdeg.SelectedValue.ToString & "', '" & par.PulisciStrSql(Me.TxtDescr.Text) & "')"
                        par.cmd.ExecuteNonQuery()

                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','INTERVENTI ADEGUAMENTO NORMATIVO')"
                        par.cmd.ExecuteNonQuery()
                        '*******************************FINE****************************************
                        '***************************************************************************
                        Session.Item("MODIFICASOTTOFORM") = 1
                    End If
                Case "UI"
                    '*************INSERT**********
                    par.cmd.CommandText = ""
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_NTERV_ADEG_NORM.NEXTVAL FROM DUAL"
                    myreader2 = par.cmd.ExecuteReader
                    If myreader2.Read Then
                        vIdAdegNorm = idGestore & myreader2(0)
                    End If
                    myreader2.Close()
                    If Me.TxtDescr.Text <> "" Then
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERV_ADEG_NORM (ID, ID_UNITA_IMMOBILIARE,COD_TIPOLOGIA,DESCRIZIONE) VALUES (" & vIdAdegNorm & ", " & vId & ", '" & Me.DrlAdeg.SelectedValue.ToString & "', '" & par.PulisciStrSql(Me.TxtDescr.Text) & "')"
                        par.cmd.ExecuteNonQuery()
                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI(ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','INTERVENTI ADEGUAMENTO NORMATIVO')"
                        par.cmd.ExecuteNonQuery()
                        '*******************************FINE****************************************
                        '***************************************************************************

                        Session.Item("MODIFICASOTTOFORM") = 1
                    End If
            End Select
            'End If
            Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)


        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = Request.QueryString("ID")
            Passato = Request.QueryString("Pas")

            'If EsistDimens = False Then
            apri()
            'Else
            '    apri()
            '    apriesist()
            'End If
        End If

    End Sub
    Private Sub apri()
        Try
            Dim ds As New Data.DataSet
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            par.OracleConn.Open()
            par.SettaCommand(par)

            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT * FROM SISCOM_MI.TIPOLOGIA_ADEGUAMENTO", par.OracleConn)
            da.Fill(ds)

            Me.DrlAdeg.DataSource = ds
            Me.DrlAdeg.DataTextField = "DESCRIZIONE"
            Me.DrlAdeg.DataValueField = "COD"
            Me.DrlAdeg.DataBind()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Public Property vId() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
 

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
    End Sub
    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
        End Set

    End Property

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        If par.IfEmpty(Me.TxtDescr.Text, "Null") <> "Null" Then
            salva()
        Else
            Response.Write("<SCRIPT>alert('Descrizione obbligatoria!');</SCRIPT>")

        End If

    End Sub
End Class
