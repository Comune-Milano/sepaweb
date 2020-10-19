Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class FSA_ElencoMandati
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String


        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='Elaborazione in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim lIdBando As String = "-1"

                par.cmd.CommandText = "SELECT * FROM BANDI_FSA ORDER BY ID DESC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lIdBando = myReader("id")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT (CASE WHEN DOMANDE_BANDO_FSA.ID_STATO = '4' THEN 'NO' ELSE 'SI' END) AS IDONEA,comuni_nazioni.nome as ""comunedi"", " _
                                    & "t_tipo_via.descrizione as ""tipovia""," _
                                    & "domande_bando_fsa.*,comp_nucleo_fsa.cod_fiscale,TO_CHAR(TO_DATE(COMP_NUCLEO_FSA.DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS DATANASCITA " _
                                    & ",DOMANDE_BANDO_FSA.quotaregionalepagata+DOMANDE_BANDO_FSA.quotacomunalepagata AS TOTCONTRIBUTO " _
                                    & "FROM t_tipo_via,domande_bando_fsa,COMP_NUCLEO_fsa,comuni_nazioni " _
                                    & "WHERE comuni_nazioni.id=domande_bando_fsa.id_luogo_rec_dnte and  t_tipo_via.cod=domande_bando_fsa.id_tipo_IND_rec_dnte (+) " _
                                    & "and COMP_NUCLEO_fsa.ID_DICHIARAZIONE=domande_bando_fsa.ID_dichiarazione and comp_nucleo_fsa.progr=domande_bando_fsa.progr_componente " _
                                    & "AND domande_bando_fsa.fl_da_liquidare='1' and domande_bando_fsa.fl_mandato_eff='0' and domande_bando_fsa.id_bando= " & lIdBando

                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    DataGridElenco.DataSource = dt
                    DataGridElenco.DataBind()
                Else
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<SCRIPT>alert('La ricerca non ha prodotto risultati!');</SCRIPT>")
                    Response.Write("<script language='javascript'> { self.close() }</script>")
                End If


                HttpContext.Current.Session.Add("AA", dt)
                imgExcel.Attributes.Add("onclick", "javascript:window.open('DownLoad.aspx?CHIAMA=1','export','');")

                par.cmd.Dispose()
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
