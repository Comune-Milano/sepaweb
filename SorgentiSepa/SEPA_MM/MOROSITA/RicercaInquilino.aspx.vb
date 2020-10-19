'*** RICERCA INQUILINO

Partial Class MOROSITA_RicercaInquilino
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sFiliale As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            'vId = 0
            '**TipoImmobile = Session.Item("BUILDING_TYPE")
            'vId = Session.Item("ID")

            Response.Flush()

            'If Session.Item("LIVELLO") <> "1" Then
            '    sFiliale = Session.Item("ID_STRUTTURA")
            'End If

            CaricaStato()

        End If

    End Sub



    'CARICO COMBO TAB_EVENTI_MOROSITA
    Private Sub CaricaStato()

        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            Me.cmbEventi.Items.Clear()

            par.cmd.CommandText = "select trim(DESCRIZIONE) as DESCRIZIONE,FUNZIONE,COD from SISCOM_MI.TAB_EVENTI_MOROSITA  " _
                               & " order by FUNZIONE asc"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                If par.IfNull(myReader1("FUNZIONE"), 0) <> 97 Then
                    '97 sarebbe DEBITO AGGIORNATO (è un evento che non è inserito in MOROSITA EVENTI (altrimente avremmo tanti di questi eventi per ogni bollettino pagato o aggiornato)
                    Me.cmbEventi.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), "M00")))
                End If
            End While
            myReader1.Close()

            Me.cmbEventi.Items.Add("TUTTI")
            Me.cmbEventi.Items.FindByText("TUTTI").Selected = True

            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim sTipoImm As String

        Try


            If Me.cmbEventi.Items.FindByText("TUTTI").Selected = True Then
                sTipoImm = ""
            Else
                sTipoImm = Me.cmbEventi.SelectedItem.Value
            End If

            Response.Write("<script>location.replace('RisultatiInquilini.aspx?CG=" & par.VaroleDaPassare(Me.txtCognome.Text) _
                                                                             & "&NM=" & par.VaroleDaPassare(Me.txtNome.Text) _
                                                                             & "&TI=" & sTipoImm _
                                                                             & "');</script>")

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub


    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


End Class
