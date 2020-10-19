
Partial Class Contratti_DettagliContr
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Dim stringa As String = "Contratto Collegato:<br /><br /><div id=" & Chr(34) & "Contenitore" & Chr(34) & " style=" & Chr(34) & "width: 100%;" & Chr(34) & " >"
        Dim contrDaCercare As String = ""
        Dim nuovo As Integer = 2
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>Reindirizzamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        IdContratto.Value = Request.QueryString("ID")

        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "select rapporti_utenza.id_domanda_abus, rapporti_utenza.cod_contratto from siscom_mi.rapporti_utenza where ID=" & IdContratto.Value

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then

                    If par.IfNull(myReader("id_domanda_abus"), -1) <> -1 Then
                        IdDomAbus.Value = par.IfNull(myReader("id_domanda_abus"), -1)
                        nuovo = 1
                    Else
                        If par.IfNull(myReader("cod_contratto"), "") <> "" Then
                            contrDaCercare = par.IfNull(myReader("cod_contratto"), "")
                        End If
                        nuovo = 0
                    End If
                End If
                myReader.Close()


                If nuovo = 1 Then
                    par.cmd.CommandText = "select tipologia_contratto_locazione.descrizione,domande_bando_vsa.contratto_num, rapporti_utenza.id, rapporti_utenza.cod_contratto,rapporti_utenza.COD_TIPOLOGIA_CONTR_LOC, SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS ""STATO"" from domande_bando_vsa, siscom_mi.rapporti_utenza,siscom_mi.tipologia_contratto_locazione where rapporti_utenza.cod_contratto=domande_bando_vsa.contratto_num and tipologia_contratto_locazione.cod=rapporti_utenza.COD_TIPOLOGIA_CONTR_LOC and domande_bando_vsa.id=" & IdDomAbus.Value
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        '   stringa = stringa & par.IfNull(myReader("COD_CONTRATTO"), "") & "-" & par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") & "-" & par.IfNull(myReader("STATO"), "") & "</br>"
                        stringa = stringa & "<a href='javascript:void(0)' onclick=" & Chr(34) & "ApriContratto2('" & par.IfNull(myReader("id"), -1) & "','" & par.IfNull(myReader("COD_CONTRATTO"), "SENZA CODICE") & "');" & Chr(34) & ">" & par.IfNull(myReader("COD_CONTRATTO"), "SENZA CODICE") & " - " & par.IfNull(myReader("descrizione"), "") & " - (" & par.IfNull(myReader("STATO"), "") & ")</a></br>"
                    End If

                End If



                If nuovo = 0 Then
                    par.cmd.CommandText = "select tipologia_contratto_locazione.descrizione,rapporti_utenza.id, rapporti_utenza.cod_contratto,rapporti_utenza.COD_TIPOLOGIA_CONTR_LOC, SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS ""STATO"" from domande_bando_vsa, siscom_mi.rapporti_utenza,siscom_mi.tipologia_contratto_locazione where rapporti_utenza.cod_contratto=domande_bando_vsa.COD_CONTRATTO_BOZZA and domande_bando_vsa.fl_autorizzazione='1' and tipologia_contratto_locazione.cod=rapporti_utenza.COD_TIPOLOGIA_CONTR_LOC AND COD_CONTRATTO_BOZZA IN (SELECT COD_CONTRATTO_BOZZA FROM DOMANDE_BANDO_VSA WHERE CONTRATTO_NUM = '" & contrDaCercare & "')"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        '  stringa = stringa & par.IfNull(myReader("COD_CONTRATTO"), "") & "-" & par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") & "-" & par.IfNull(myReader("STATO"), "") & "</br>"
                        stringa = stringa & "<a href='javascript:void(0)' onclick=" & Chr(34) & "ApriContratto2('" & par.IfNull(myReader("id"), -1) & "','" & par.IfNull(myReader("COD_CONTRATTO"), "SENZA CODICE") & "');" & Chr(34) & ">" & par.IfNull(myReader("COD_CONTRATTO"), "SENZA CODICE") & " - " & par.IfNull(myReader("descrizione"), "") & " - (" & par.IfNull(myReader("STATO"), "") & ")</a></br>"
                    End If

                End If

                Label1.Text = "<br />" & stringa

                '   par.cmd.CommandText = "select domande_bando_vsa.cod_contratto_abus, rapporti_utenza.id, rapporti_utenza.cod_contratto,rapporti_utenza.COD_TIPOLOGIA_CONTR_LOC, SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS ""STATO"" from domande_bando_vsa, siscom_mi.rapporti_utenza where rapporti_utenza.cod_contratto=domande_bando_vsa.contratto_num and domande_bando_vsa.fl_autorizzazione='1' and rapporti_utenza.id=" & IdContratto.Value

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
