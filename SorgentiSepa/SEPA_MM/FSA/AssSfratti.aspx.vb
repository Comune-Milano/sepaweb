
Partial Class FSA_AssSfratti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try

                Dim Str As String

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Response.Write("<table width='100%'>")
                Response.Write("<tr>")
                Response.Write("<td style='text-align: center'>")
                Response.Write("<strong><span style='font-size: 14pt; font-family: Arial'>Elenco Correlazioni FSA-ERP</span></strong></td>")
                Response.Write("</tr>")
                Response.Write("</table>")

                Response.Write("<br />")
                Response.Write("<table width='100%'>")

                Response.Write("<tr>")
                Response.Write("<td>")
                Response.Write("<span style='font-family: Arial'>Protocollo FSA</span></td>")

                Response.Write("<td>")
                Response.Write("<span style='font-family: Arial'>Nominativo</span></td>")
                Response.Write("<td>")
                Response.Write("<span style='font-family: Arial'>Cod. Fiscale</span></td>")
                Response.Write("<td><span style='font-family: Arial'>Protocollo ERP</span></td>")
                Response.Write("<td><span style='font-family: Arial'>Tipo</span></td>")
                Response.Write("</tr>")

                Dim ID_BANDO As Long = 6

                par.cmd.CommandText = "SELECT MAX(ID) FROM BANDI_FSA"
                Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader123.Read() Then
                    ID_BANDO = myReader123(0)
                End If
                myReader123.Close()

                par.cmd.CommandText = "SELECT DOMANDE_BANDO.ID as ""id_domanda"",COMP_NUCLEO_FSA.COD_FISCALE,DOMANDE_BANDO_FSA.PG,COMP_NUCLEO_FSA.COGNOME,COMP_NUCLEO_FSA.NOME,DOMANDE_BANDO.PG AS ""PG_ERP"" FROM COMP_NUCLEO_FSA,DOMANDE_BANDO_FSA,COMP_NUCLEO,DOMANDE_BANDO WHERE domande_bando_fsa.id_bando=" & ID_BANDO & " and DOMANDE_BANDO_FSA.id_stato<>'4' and DOMANDE_BANDO_FSA.ID_DICHIARAZIONE=COMP_NUCLEO_FSA.ID_DICHIARAZIONE AND COMP_NUCLEO_FSA.COD_FISCALE=COMP_NUCLEO.COD_FISCALE AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE and domande_bando.id in (select id_PRATICA from domande_categorie where substr(descrizione,1,8)='SFRATTO')"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read()
                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("pg"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("cod_fiscale"), "") & "</span></td>")
                    Response.Write("<td><span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("pg_erp"), "") & "</span></td>")
                    Response.Write("<td><span style='font-size: 10pt;font-family: Arial'>CAT. SOCIALE SFRATTO</span></td>")
                    Response.Write("</tr>")
                Loop
                myReader.Close()
                par.cmd.CommandText = "SELECT DOMANDE_BANDO.ID as ""id_domanda"",COMP_NUCLEO_FSA.COD_FISCALE,DOMANDE_BANDO_FSA.PG,COMP_NUCLEO_FSA.COGNOME,COMP_NUCLEO_FSA.NOME,DOMANDE_BANDO.PG AS ""PG_ERP"" FROM COMP_NUCLEO_FSA,DOMANDE_BANDO_FSA,COMP_NUCLEO,DOMANDE_BANDO WHERE domande_bando_fsa.id_bando=" & ID_BANDO & " and  DOMANDE_BANDO_FSA.id_stato<>'4' and DOMANDE_BANDO_FSA.ID_DICHIARAZIONE=COMP_NUCLEO_FSA.ID_DICHIARAZIONE AND COMP_NUCLEO_FSA.COD_FISCALE=COMP_NUCLEO.COD_FISCALE AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE and domande_bando.id_PARA_7<>-1"
                myReader = par.cmd.ExecuteReader()
                Do While myReader.Read()
                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("pg"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("cod_fiscale"), "") & "</span></td>")
                    Response.Write("<td><span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("pg_erp"), "") & "</span></td>")
                    Response.Write("<td><span style='font-size: 10pt;font-family: Arial'>PUNTEGGIO SFRATTO</span></td>")
                    Response.Write("</tr>")
                Loop

                myReader.Close()
                par.cmd.CommandText = "SELECT DOMANDE_BANDO.ID as ""id_domanda"",COMP_NUCLEO_FSA.COD_FISCALE,DOMANDE_BANDO_FSA.PG,COMP_NUCLEO_FSA.COGNOME,COMP_NUCLEO_FSA.NOME,DOMANDE_BANDO.PG AS ""PG_ERP"" FROM COMP_NUCLEO_FSA,DOMANDE_BANDO_FSA,COMP_NUCLEO,DOMANDE_BANDO WHERE domande_bando_fsa.id_bando=" & ID_BANDO & " and  DOMANDE_BANDO_FSA.id_stato<>'4' and DOMANDE_BANDO_FSA.ID_DICHIARAZIONE=COMP_NUCLEO_FSA.ID_DICHIARAZIONE AND COMP_NUCLEO_FSA.COD_FISCALE=COMP_NUCLEO.COD_FISCALE AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE and domande_bando.id IN  (select id_DOMANDA from DEROGHE_ART_14 where ID_TIPO=0 OR ID_TIPO=1)"
                myReader = par.cmd.ExecuteReader()
                Do While myReader.Read()
                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("pg"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("cod_fiscale"), "") & "</span></td>")
                    Response.Write("<td><span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("pg_erp"), "") & "</span></td>")
                    Response.Write("<td><span style='font-size: 10pt;font-family: Arial'>ART.14 A o B</span></td>")
                    Response.Write("</tr>")
                Loop
                Response.Write("</table>")

                myReader.Close()
                par.cmd.CommandText = "SELECT DOMANDE_BANDO.ID as ""id_domanda"",COMP_NUCLEO_FSA.COD_FISCALE,DOMANDE_BANDO_FSA.PG,COMP_NUCLEO_FSA.COGNOME,COMP_NUCLEO_FSA.NOME,DOMANDE_BANDO.PG AS ""PG_ERP"" FROM COMP_NUCLEO_FSA,DOMANDE_BANDO_FSA,COMP_NUCLEO,DOMANDE_BANDO WHERE domande_bando_fsa.id_bando=" & ID_BANDO & " and  DOMANDE_BANDO_FSA.id_stato<>'4' and DOMANDE_BANDO_FSA.ID_DICHIARAZIONE=COMP_NUCLEO_FSA.ID_DICHIARAZIONE AND COMP_NUCLEO_FSA.COD_FISCALE=COMP_NUCLEO.COD_FISCALE AND DOMANDE_BANDO.ID_DICHIARAZIONE=COMP_NUCLEO.ID_DICHIARAZIONE and domande_bando.id in (select id from SFRATTI_ESECUTIVI where DATA_CONVALIDA_SFRATTO IS NOT NULL)"
                myReader = par.cmd.ExecuteReader()
                Do While myReader.Read()
                    Response.Write("<tr>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("pg"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & "</span></td>")
                    Response.Write("<td>")
                    Response.Write("<span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("cod_fiscale"), "") & "</span></td>")
                    Response.Write("<td><span style='font-size: 10pt;font-family: Arial'>" & par.IfNull(myReader("pg_erp"), "") & "</span></td>")
                    Response.Write("<td><span style='font-size: 10pt;font-family: Arial'>DATA CONVALIDA SFRATTO</span></td>")
                    Response.Write("</tr>")
                Loop
                Response.Write("</table>")

                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If

    End Sub
End Class
