
Partial Class Contratti_SchemaAltriAnni
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        ' If Not IsPostBack Then
        Try
            Dim ColoreRiga As String = "#FFFFFF"
            If Request.QueryString("ID") <> Nothing Then
                par.OracleConn = CType(HttpContext.Current.Session.Item(Request.QueryString("CN")), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("CN")), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                Label1.Text = ""
                par.cmd.CommandText = "select distinct anno FROM SISCOM_MI.bol_schema WHERE ID_contratto=" & Request.QueryString("ID") & " and anno<>" & Request.QueryString("A") & " order by anno desc"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReaderA.Read

                    ColoreRiga = "#FFFFFF"
                    Label1.Text = Label1.Text & "<tr><td><table style='width:100%;'><tr><td bgcolor='#CCCCCC' class='style1'>ANNO " & par.IfNull(myReaderA("anno"), "") & "</td></tr></table></td></tr><tr><td><table style='width:100%;'><tr bgcolor='#FFFFCC'><td style='font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='40%'>VOCE</td><td style='font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>DA RATA-PER RATE</td><td style='text-align: right;font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>IMPORTO SINGOLA RATA</td><td style='text-align: right;font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>AZIONE</td></tr></table></td></tr>"

                    par.cmd.CommandText = "select t_voci_bolletta.descrizione,bol_schema.* from siscom_mi.t_voci_bolletta,siscom_mi.bol_schema where t_voci_bolletta.id=bol_schema.id_voce and anno=" & myReaderA("anno") & " and id_contratto=" & Request.QueryString("ID")
                    Dim myReaderAA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReaderAA.Read
                        If myReaderA("anno") > Year(Now) Then
                            If myReaderAA("FL_PREVENTIVI") = "0" Then
                                Label1.Text = Label1.Text & "<tr bgcolor='" & ColoreRiga & "'><td><table style='width:100%;'><tr><td style='font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='40%'>" & par.IfNull(myReaderAA("descrizione"), "") & "</td><td style='font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>" & par.IfNull(myReaderAA("da_rata"), "") & " - " & par.IfNull(myReaderAA("per_rate"), "") & "</td><td style='text-align: right;font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>" & Format(par.IfNull(myReaderAA("importo_singola_rata"), "0,00"), "0.00") & "</td><td style='text-align: right;font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'><a href='EliminaVoceSchema.aspx?CN=" & Request.QueryString("CN") & "&ID=" & par.CriptaMolto(par.IfNull(myReaderAA("id"), "-1")) & "' target='_blank'>Elimina</a></td></tr></table></td></tr>"
                            Else
                                Label1.Text = Label1.Text & "<tr bgcolor='" & ColoreRiga & "'><td><table style='width:100%;'><tr><td style='font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='40%'>" & par.IfNull(myReaderAA("descrizione"), "") & "</td><td style='font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>" & par.IfNull(myReaderAA("da_rata"), "") & " - " & par.IfNull(myReaderAA("per_rate"), "") & "</td><td style='text-align: right;font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>" & Format(par.IfNull(myReaderAA("importo_singola_rata"), "0,00"), "0.00") & "</td><td style='text-align: right;font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>--</td></tr></table></td></tr>"
                            End If
                        Else
                            Label1.Text = Label1.Text & "<tr bgcolor='" & ColoreRiga & "'><td><table style='width:100%;'><tr><td style='font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='40%'>" & par.IfNull(myReaderAA("descrizione"), "") & "</td><td style='font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>" & par.IfNull(myReaderAA("da_rata"), "") & " - " & par.IfNull(myReaderAA("per_rate"), "") & "</td><td style='text-align: right;font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>" & Format(par.IfNull(myReaderAA("importo_singola_rata"), "0,00"), "0.00") & "</td><td style='text-align: right;font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt; font-weight: bold' width='20%'>--</td></tr></table></td></tr>"
                        End If
                        If ColoreRiga = "#FFFFFF" Then
                            ColoreRiga = "#DFEFFF"
                        Else
                            ColoreRiga = "#FFFFFF"
                        End If
                    Loop
                    myReaderAA.Close()

                    'Label1.Text = Label1.Text & "</table>"


                Loop
                myReaderA.Close()
                If Label1.Text = "" Then Label1.Text = "Nessuno schema bollette trovato!"
            Else
                Label1.Text = "Nessuno schema bollette trovato!"
            End If


        Catch ex As Exception

            Label1.Text = ex.Message
        End Try
        'End If


    End Sub
End Class
