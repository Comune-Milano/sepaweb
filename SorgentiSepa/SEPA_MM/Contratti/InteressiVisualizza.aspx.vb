Imports System.Collections.Generic

Partial Class Contratti_Interessi
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try

                par.OracleConn.Open()
                par.SettaCommand(par)

                cmbComplesso.Items.Clear()
                cmbEdificio.Items.Clear()
                cmbUnita.Items.Clear()


                cmbComplesso.Items.Add(New ListItem("TUTTI", -1))
                cmbEdificio.Items.Add(New ListItem("TUTTI", -1))
                cmbUnita.Items.Add(New ListItem("TUTTI", -1))

                par.cmd.CommandText = "SELECT id,denominazione FROM SISCOM_MI.complessi_immobiliari order by denominazione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read

                    cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                cmbComplesso.SelectedIndex = -1
                cmbComplesso.Items.FindByValue("-1").Selected = True


                par.OracleConn.Close()
            Catch ex As Exception
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try
        End If
        txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        'txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        Try


            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            cmbEdificio.Items.Clear()
            cmbUnita.Items.Clear()

            cmbEdificio.Items.Add(New ListItem("TUTTI", -1))
            cmbUnita.Items.Add(New ListItem("TUTTI", -1))

            par.cmd.CommandText = "SELECT distinct(id),denominazione FROM SISCOM_MI.edifici where id_complesso=" & cmbComplesso.SelectedValue & " order by denominazione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            cmbUnita.Items.Clear()
            cmbUnita.Items.Add(New ListItem("TUTTI", -1))

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.unita_immobiliari where id_edificio=" & cmbEdificio.SelectedValue & " order by scala asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbUnita.Items.Add(New ListItem(par.IfNull(myReader1("COD_UNITA_IMMOBILIARE"), " "), par.IfNull(myReader1("ID"), "-1")))
            End While
            myReader1.Close()
            par.OracleConn.Close()

        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub imgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        Try
            Dim sAggiunta As String = ""
            Dim sAggiunta1 As String = ""

            If cmbComplesso.SelectedItem.Value <> "-1" Then
                sAggiunta = "EDIFICI.ID_COMPLESSO=" & cmbComplesso.SelectedItem.Value
            End If

            If cmbEdificio.SelectedItem.Value <> "-1" Then
                If sAggiunta <> "" Then sAggiunta = sAggiunta & " AND "
                sAggiunta = "UNITA_CONTRATTUALE.ID_EDIFICIO=" & cmbEdificio.SelectedItem.Value
            End If

            If cmbUnita.SelectedItem.Value <> "-1" Then
                If sAggiunta <> "" Then sAggiunta = sAggiunta & " AND "
                sAggiunta = "UNITA_CONTRATTUALE.COD_UNITA_IMMOBILIARE='" & cmbUnita.SelectedItem.Text & "' "
            End If
            If sAggiunta <> "" Then sAggiunta = sAggiunta & " AND "

            If txtData.Text <> "" Then
                sAggiunta1 = " and ADEGUAMENTO_INTERESSI.DATA>='" & par.AggiustaData(txtData.Text) & "' "
            End If

            If txtDataAl.Text <> "" Then
                If sAggiunta1 <> "" Then sAggiunta1 = sAggiunta1 & " AND "
                sAggiunta1 = sAggiunta1 & " ADEGUAMENTO_INTERESSI.DATA<='" & par.AggiustaData(txtDataAl.Text) & "' "
            End If


            Dim Str As String = "select rapporti_utenza.cod_contratto,ADEGUAMENTO_INTERESSI.DATA,ADEGUAMENTO_INTERESSI.IMPORTO AS ""TOTALE"",ADEGUAMENTO_INTERESSI_voci.* from SISCOM_MI.rapporti_utenza,SISCOM_MI.ADEGUAMENTO_INTERESSI,SISCOM_MI.ADEGUAMENTO_INTERESSI_voci where rapporti_utenza.id=adeguamento_interessi.id_contratto and ADEGUAMENTO_INTERESSI_voci.id_adeguamento=ADEGUAMENTO_INTERESSI.id " & sAggiunta1 & " AND adeguamento_interessi.id_contratto in (select RAPPORTI_UTENZA.ID FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE " & sAggiunta & "  UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO) order by id_contratto,dal asc"

            HttpContext.Current.Session.Add("BB", Str)
            Response.Write("<script>var fin;fin=window.open('VisInteressi.aspx');fin.focus();</script>")



        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:Visualizza Interessi Deposito - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub
End Class
