
Partial Class AMMSEPA_ElencoBollette
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                Dim MIOCOLORE As String = "#C0C0C0"
                Dim Nfile As String = par.DeCripta(Request.QueryString("F"))
                Dim i As Long = 0
                Dim numerorata As String = ""

                If InStr(UCase(Nfile), "ESITI") > 0 Then
                    Nfile = Mid(Nfile, InStr(UCase(Nfile), "ESITI"), Len(Nfile))
                Else
                    Nfile = Mid(Nfile, InStr(UCase(Nfile), "INCASSI"), Len(Nfile))
                End If
                Response.Write("Elenco Bollette elaborate con il file " & Nfile & "<br/><br/><table style='width:100%;'>")
                Response.Write("<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td></td><td>CONTRATTO</td><td>INTESTATARIO</td><td>RATA</td><td>PERIODO</td><td>NOTE</td><td>INDIRIZZO</td><td>CAP-CITTA</td><td>DATA PAGAMENTO</td><td>DATA VALUTA</td><td>DATA VALUTA CR.</td><td>IMPORTO BOLLETTA</td><td>IMPORTO PAGATO</td></tr>")

                par.OracleConn.Open()
                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand("select bol_bollette.*,rapporti_utenza.cod_contratto from siscom_mi.rapporti_utenza,siscom_mi.bol_bollette where bol_bollette.id_contratto=rapporti_utenza.id and  upper(rif_file_rendiconto)='" & UCase(Nfile) & "' order by intestatario asc", par.OracleConn)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
                While myReader.Read()
                    Select Case par.IfNull(myReader("n_rata"), " ")
                        Case "99"
                            numerorata = "MA"
                        Case "999"
                            numerorata = "AU"
                        Case "99999"
                            numerorata = "CO"
                        Case Else
                            numerorata = par.IfNull(myReader("n_rata"), " ")
                    End Select
                    Response.Write("<tr style='background-color: " & MIOCOLORE & ";font-family: ARIAL; font-size: 8pt; font-weight: bold'><td>" & i & ")</td><td>" & par.IfNull(myReader("cod_contratto"), " ") & "</td><td>" & par.IfNull(myReader("intestatario"), " ") & "</td><td>" & numerorata & "/" & par.IfNull(myReader("anno"), " ") & "</td><td>" & par.FormattaData(par.IfNull(myReader("riferimento_da"), " ")) & "-" & par.FormattaData(par.IfNull(myReader("riferimento_a"), " ")) & "</td><td>" & par.IfNull(myReader("NOTE"), " ") & "</td><td>" & par.IfNull(myReader("indirizzo"), " ") & "</td><td>" & par.IfNull(myReader("cap_citta"), " ") & "</td><td>" & par.FormattaData(par.IfNull(myReader("data_PAGAMENTO"), " ")) & "</td><td>" & par.FormattaData(par.IfNull(myReader("data_valuta"), " ")) & "</td><td>" & par.FormattaData(par.IfNull(myReader("data_valuta_creditore"), " ")) & "</td><td>" & par.IfNull(myReader("IMPORTO_TOTALE"), "0,00") & "</td><td>" & par.IfNull(myReader("IMPORTO_PAGATO"), "0,00") & "</td></tr>")
                    i = i + 1
                    If MIOCOLORE = "#C0C0C0" Then
                        MIOCOLORE = "#ffffff"
                    Else
                        MIOCOLORE = "#C0C0C0"
                    End If
                End While

                Response.Write("</table>")
                cmd.Dispose()
                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub
End Class
