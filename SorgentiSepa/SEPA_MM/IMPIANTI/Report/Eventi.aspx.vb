
Partial Class FSA_Eventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String

    Dim vIdImpianto As String

    'Dim sValoreComplesso As String
    'Dim sValoreEdificio As String
    'Dim sValoreImpianto As String
    'Dim sVerifiche As String

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Try
                lblTitolo.Text = "ELENCO EVENTI IMPIANTI"

                vIdImpianto = Request.QueryString("ID_IMPIANTO")

                'sValoreComplesso = Request.QueryString("CO")
                'sValoreEdificio = Request.QueryString("ED")
                'sValoreImpianto = Request.QueryString("IM")
                'sVerifiche = Request.QueryString("VER")


                sStringaSql = "select TO_DATE(SISCOM_MI.EVENTI_IMPIANTI.DATA_ORA,'yyyyMMddHH24MISS') as ""DATA_ORA""," _
                                  & " SISCOM_MI.TAB_EVENTI.DESCRIZIONE,SISCOM_MI.EVENTI_IMPIANTI.COD_EVENTO,SISCOM_MI.EVENTI_IMPIANTI.MOTIVAZIONE," _
                                  & " SEPA.OPERATORI.OPERATORE,SISCOM_MI.EVENTI_IMPIANTI.ID_OPERATORE,SEPA.CAF_WEB.COD_CAF " _
                           & " from SEPA.CAF_WEB, SISCOM_MI.EVENTI_IMPIANTI, SISCOM_MI.TAB_EVENTI,SEPA.OPERATORI " _
                           & " where SISCOM_MI.EVENTI_IMPIANTI.ID_IMPIANTO= " & vIdImpianto _
                             & " and SISCOM_MI.EVENTI_IMPIANTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI.COD (+) " _
                             & " and SISCOM_MI.EVENTI_IMPIANTI.ID_OPERATORE=SEPA.OPERATORI.ID (+) " _
                             & " and SEPA.CAF_WEB.ID=SEPA.OPERATORI.ID_CAF " _
                          & " order by EVENTI_IMPIANTI.DATA_ORA desc, EVENTI_IMPIANTI.COD_EVENTO desc"


                par.OracleConn.Open()
                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

                lblTotale.Text = "0"
                Do While myReader.Read()
                    lblTotale.Text = CInt(lblTotale.Text) + 1
                Loop

                lblTotale.Text = "TOTALE EVENTI TROVATI: " & lblTotale.Text
                myReader.Close()

                '*** CARICO LA GRIGLIA
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds)
                DataGrid1.DataSource = ds
                DataGrid1.DataBind()
                ds.Dispose()
                '*******************************


                par.cmd.Dispose()
                par.OracleConn.Close()
                par.OracleConn.Dispose()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch ex As Exception
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End Try
        End If

    End Sub

    Private Sub Visualizza(ByVal IdImpianto As Long, ByVal Codice As String)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO WHERE RAPPORTI_UTENZA.ID=" & IdContratto
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            Response.Write("Impianti, CODICE: <strong>" & Codice & "</strong><br />")
            Response.Write("<br />")
            'End If
            'myReader.Close()

            Response.Write("<table width='100%'>")
            Response.Write("<tr>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DATA</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>DESCRIZIONE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>MOTIVAZIONE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>OPERATORE</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>ENTE</strong></span></td>")

            Response.Write("</tr>")

            par.cmd.CommandText = "select TO_CHAR(TO_DATE(EVENTI_IMPIANTI.DATA_ORA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_ORA""," _
                                      & " TAB_EVENTI.DESCRIZIONE,EVENTI_IMPIANTI.COD_EVENTO,EVENTI_IMPIANTI.MOTIVAZIONE," _
                                      & " OPERATORI.OPERATORE,EVENTI_IMPIANTI.ID_OPERATORE,CAF_WEB.COD_CAF " _
                               & " from CAF_WEB, siscom_mi.EVENTI_IMPIANTI, siscom_mi.TAB_EVENTI,SEPA.OPERATORI " _
                               & " where EVENTI_IMPIANTI.ID_IMPIANTO= " & IdImpianto _
                                 & " and EVENTI_IMPIANTI.COD_EVENTO=TAB_EVENTI.COD (+) " _
                                 & " and EVENTI_IMPIANTI.ID_OPERATORE=OPERATORI.ID (+) " _
                                 & " and CAF_WEB.ID=OPERATORI.ID_CAF " _
                              & " order by EVENTI_IMPIANTI.DATA_ORA desc"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            Do While myReader.Read()

                MiaData = Mid(par.IfNull(myReader("DATA_ORA"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 1, 4)
                If IsDate(MiaData) = True Then
                    MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 11, 2)
                Else
                    MiaData = ""
                End If

                OPERATORE = par.IfNull(myReader("OPERATORE"), "")

                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_EVENTO"), "") & " - " & par.IfNull(myReader("DESCRIZIONE"), "") & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_CAF"), "") & "</span></td>")

                Response.Write("</tr>")

            Loop
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Sub


End Class
