
Partial Class Contratti_VisInteressi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable



    Private Property NomeFile() As String
        Get
            If Not (ViewState("par_NomeFile") Is Nothing) Then
                Return CStr(ViewState("par_NomeFile"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_NomeFile") = value
        End Set

    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then


                Dim Str As String
                Dim SS As Integer = 0
                Dim I = 0

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()
                NomeFile = "Interessi"

                '********CONNESSIONE*********
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                ''Dim myReader As Oracle.DataAccess.Client.OracleDataReader

                'par.cmd.CommandText = CType(HttpContext.Current.Session.Item("BB"), String)


                'Dim row As System.Data.DataRow
                'Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                'Dim dt2 As New Data.DataTable

                'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                'da.Fill(dt)

                'If dt.Rows.Count > 0 Then
                '    DataGridRateEmesse.DataSource = dt
                '    DataGridRateEmesse.DataBind()
                '    par.OracleConn.Close()
                'End If


                'HttpContext.Current.Session.Add("AA", dt)
                'imgExcel.Attributes.Add("onclick", "javascript:window.open('Report/DownLoad.aspx?CHIAMA=100','Distinta','');")

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                '                par.cmd.CommandText = "select ADEGUAMENTO_INTERESSI.*,ADEGUAMENTO_INTERESSI_voci.* from ADEGUAMENTO_INTERESSI,ADEGUAMENTO_INTERESSI_voci where ADEGUAMENTO_INTERESSI_voci.id_adeguamento=ADEGUAMENTO_INTERESSI.id order by id_contratto,dal asc"
                par.cmd.CommandText = CType(HttpContext.Current.Session.Item("BB"), String)
                myReader = par.cmd.ExecuteReader()

                Dim S As String = ""


                Response.Write("<strong><span style='font-family: Arial'>PROSPETTO INTERESSI DEPOSITO CAUZIONALE</span></strong>")
                Response.Write("<br />")
                Response.Write("<br />")
                Response.Write("<br />")
                Response.Write("<br />")

                Response.Write("<ul>")
                Do While myReader.Read



                    If S <> par.IfNull(myReader("COD_CONTRATTO"), "") Then
                        Response.Write("<table style='background-color: gainsboro' cellpadding='0' cellspacing='0' width='100%'>")
                        Response.Write("<tr>")

                        Response.Write("<td style='width: 200px'><span style='font-size: 12pt;font-family: Arial'>Contratto:" & par.IfNull(myReader("COD_CONTRATTO"), "") & "</span></td>")
                        Response.Write("<td style='width: 200px'><span style='font-size: 12pt;font-family: Arial'>Calcolato fino al:" & par.FormattaData(par.IfNull(myReader("data"), "")) & "</span></td>")
                        Response.Write("<td style='width: 200px'><span style='font-size: 12pt;font-family: Arial'>Totale Importo:" & par.IfNull(myReader("TOTALE"), "") & "</span></td>")
                        S = par.IfNull(myReader("COD_CONTRATTO"), "")

                        Response.Write("</tr>")
                        Response.Write("</table>")
                        SS = 0


                    Else
                        'Response.Write("<td style='width: 100px'><span style='font-size: 12pt;font-family: Arial'>&nbsp;</span></td>")
                        'Response.Write("<td style='width: 100px'><span style='font-size: 12pt;font-family: Arial'>&nbsp;</span></td>")
                        'Response.Write("<td style='width: 100px'><span style='font-size: 12pt;font-family: Arial'>&nbsp;</span></td>")
                        SS = 1

                    End If

                    Response.Write("<ul>")
                    Response.Write("<table style='border-bottom: black 1px dashed' cellpadding='0' cellspacing='0' width='100%'>")

                    If SS = 0 Then

                        Response.Write("<tr>")
                        Response.Write("<td style='width: 150px'><span style='font-size: 10pt;font-family: Arial'>DAL</span></td>")
                        Response.Write("<td style='width: 150px'><span style='font-size: 10pt;font-family: Arial'>AL</span></td>")
                        Response.Write("<td style='width: 150px'><span style='font-size: 10pt;font-family: Arial'>GIORNI</span></td>")
                        Response.Write("<td style='width: 150px'><span style='font-size: 10pt;font-family: Arial'>TASSO INTERESSE</span></td>")
                        Response.Write("<td style='width: 200px'><span style='font-size: 10pt;font-family: Arial'>INTERESSE CALCOLATO</span></td>")
                        Response.Write("</tr>")
                    End If

                    Response.Write("<tr>")
                    Response.Write("<td style='width: 150px'><span style='font-size: 10pt;font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DAL"), "")) & "</span></td>")
                    Response.Write("<td style='width: 150px'><span style='font-size: 10pt;font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("AL"), "")) & "</span></td>")
                    Response.Write("<td style='width: 150px'><span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("GIORNI"), "") & "</span></td>")
                    Response.Write("<td style='width: 150px'><span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("TASSO"), "0") & "</span></td>")
                    Response.Write("<td style='width: 200px'><span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("IMPORTO"), "") & "</span></td>")
                    Response.Write("</tr>")
                    Response.Write("</table>")
                    Response.Write("</ul>")



                Loop
                Response.Write("</ul>")
                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try

    End Sub
End Class
