﻿
Partial Class CENSIMENTO_VariazConfig
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim EsistDimens As Boolean = False
    Dim ValoreCaricato As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = Request.QueryString("ID")
            Passato = Request.QueryString("Pas")
            If EsistDimens = False Then
                apri()
            Else
                apri()
                apriesist()
            End If
        End If

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

    Private Sub apri()
        Try
            Dim ds As New Data.DataSet
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter


            par.OracleConn.Open()
            par.SettaCommand(par)

            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT * FROM SISCOM_MI.TIPOLOGIA_VARIAZIONE_CONFIG", par.OracleConn)
            da.Fill(ds)

            Me.DrlTipoVariaz.DataSource = ds
            Me.DrlTipoVariaz.DataTextField = "DESCRIZIONE"
            Me.DrlTipoVariaz.DataValueField = "COD"
            Me.DrlTipoVariaz.DataBind()

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

    Private Sub apriesist()
        Try


            Dim myReader As Oracle.DataAccess.Client.OracleDataReader

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "select cod_TIPOLOGIA, DESCRIZIONE from SISCOM_MI.VARIAZIONI_CONFIGURAZIONE where ID_EDIFICIO = " & vId
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                Me.TxtVariaz.Text = myReader("DESCRIZIONE")
                Me.DrlTipoVariaz.SelectedValue = -1
                Me.DrlTipoVariaz.Items.FindByValue(myReader("COD_TIPOLOGIA")).Selected = True
                ValoreCaricato = Me.TxtVariaz.Text

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub


    Private Sub salva()
        Try
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader
            Dim vIdVAriazConfig As String
            Dim idGestore = Mid(vId, 1, 1)

            'RIAPERTURA DELLA STESSA CONNESSIONE E TRANSAZIONE

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            If Me.DrlTipoVariaz.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.TxtVariaz.Text, "Null") <> "Null" Then

                Select Case Passato
                    Case "ED"

                        '*************INSERT**********
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_VARIAZIONI_CONFIGURAZIONE.NEXTVAL FROM DUAL"
                        myreader2 = par.cmd.ExecuteReader
                        If myreader2.Read Then
                            vIdVAriazConfig = idGestore & myreader2(0)
                        End If
                        myreader2.Close()
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.VARIAZIONI_CONFIGURAZIONE (ID, ID_EDIFICIO,COD_TIPOLOGIA,DESCRIZIONE) VALUES (" & vIdVAriazConfig & ", " & vId & ", '" & Me.DrlTipoVariaz.SelectedValue.ToString & "', '" & par.PulisciStrSql(Me.TxtVariaz.Text) & "'  )"
                        par.cmd.ExecuteNonQuery()
                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','VARIAZIONI DI CONFIGURAZIONE')"
                        par.cmd.ExecuteNonQuery()
                        '*******************************FINE****************************************
                        '***************************************************************************

                        Session.Item("MODIFICASOTTOFORM") = 1
                        'Response.Write("<SCRIPT>alert('Elemento salvato correttamente!');</SCRIPT>")
                        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

                    Case "UI"
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_VARIAZIONI_CONFIGURAZIONE.NEXTVAL FROM DUAL"
                        myreader2 = par.cmd.ExecuteReader
                        If myreader2.Read Then
                            vIdVAriazConfig = idGestore & myreader2(0)
                        End If
                        myreader2.Close()
                        par.cmd.CommandText = ""
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.VARIAZIONI_CONFIGURAZIONE (ID, ID_UNITA,COD_TIPOLOGIA,DESCRIZIONE) VALUES (" & vIdVAriazConfig & ", " & vId & ", '" & Me.DrlTipoVariaz.SelectedValue.ToString & "', '" & par.PulisciStrSql(Me.TxtVariaz.Text) & "'  )"
                        par.cmd.ExecuteNonQuery()
                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI(ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','VARIAZIONI DI CONFIGURAZIONE')"
                        par.cmd.ExecuteNonQuery()
                        '*******************************FINE****************************************
                        '***************************************************************************

                        Session.Item("MODIFICASOTTOFORM") = 1
                        'Response.Write("<SCRIPT>alert('Elemento salvato correttamente!');</SCRIPT>")
                        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

                End Select

            Else
                Response.Write("<SCRIPT>alert('Riempire i campi obbligatori!');</SCRIPT>")

            End If

            'End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)

        End Try
    End Sub
    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        Me.salva()
        ' Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Write("<script>opener.document.getElementById('form1').submit();window.close();</script>")
    End Sub
End Class
