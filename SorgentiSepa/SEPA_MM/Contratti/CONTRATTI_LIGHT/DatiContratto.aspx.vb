
Partial Class Contratti_CONTRATTI_LIGHT_DatiContratto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE_AU_LIGHT") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            If par.DeCriptaMolto(Request.QueryString("LT")) <> "LETTURA" Then
                If Request.QueryString("LT") = "46412446461946416791764641641971944194946548928525822828652525255878787897987987" Then
                Else
                    Response.Redirect("~/AccessoNegato.htm", True)
                    Exit Sub
                End If
            Else
            End If
            IndiceContratto = Request.QueryString("id")
            CaricaDati()
        End If
    End Sub

    Private Sub CaricaDati()
        Try
            Dim Intestatario As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)



            par.cmd.CommandText = "select indirizzi.descrizione,indirizzi.civico,indirizzi.cap,indirizzi.localita, unita_immobiliari.cod_unita_immobiliare,siscom_mi.getintestatari(rapporti_utenza.id) as intestatario,rapporti_utenza.cod_contratto from siscom_mi.indirizzi, siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari,siscom_mi.rapporti_utenza where indirizzi.id (+)=unita_immobiliari.id_indirizzo and unita_contrattuale.id_contratto (+)=rapporti_utenza.id and unita_contrattuale.id_unita_principale is null and unita_immobiliari.id (+)=unita_contrattuale.id_unita and rapporti_utenza.id=" & IndiceContratto
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Intestatario = par.IfNull(myReader("intestatario"), ",")
                lblDatiContratto.Text = par.IfNull(myReader("cod_contratto"), "---")
                lblIntestatario.Text = Mid(Intestatario, 1, Len(Intestatario) - 1)

                lblDatiUnita.Text = par.IfNull(myReader("cod_unita_immobiliare"), "---")
                lblIndirizzo.Text = par.IfNull(myReader("descrizione"), "") & " " & par.IfNull(myReader("civico"), "") & " - " & par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("localita"), "")
            End If
            myReader.Close()

            sStringaSQL1 = "select RAPPORTI_UTENZA_ARCHIVIO.ID,TAB_GESTORI_ARCHIVIO.DESCRIZIONE AS GESTORE,RAPPORTI_UTENZA_ARCHIVIO.COD_EUSTORGIO,RAPPORTI_UTENZA_ARCHIVIO.FALDONE,RAPPORTI_UTENZA_ARCHIVIO.SCATOLA,RAPPORTI_UTENZA_ARCHIVIO.ID_CONTRATTO,TAB_CAT_EUSTORGIO.DESCRIZIONE AS CATEGORIA from SISCOM_MI.TAB_GESTORI_ARCHIVIO, SISCOM_MI.TAB_CAT_EUSTORGIO,siscom_mi.rapporti_utenza,siscom_mi.rapporti_utenza_archivio where TAB_GESTORI_ARCHIVIO.COD (+)=RAPPORTI_UTENZA_ARCHIVIO.GESTORE AND TAB_CAT_EUSTORGIO.COD(+)=RAPPORTI_UTENZA_ARCHIVIO.CATEGORIA AND rapporti_utenza_archivio.id_contratto (+)=rapporti_utenza.id and rapporti_utenza.id=" & IndiceContratto & " order by cod_eustorgio asc"
            BindGrid()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.RAPPORTI_UTENZA_ARCHIVIO")
            Datagrid2.DataSource = ds
            Datagrid2.DataBind()

        Catch ex As Exception
            par.OracleConn.Close()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub


    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property


    Public Property IndiceContratto() As String
        Get
            If Not (ViewState("par_IndiceContratto") Is Nothing) Then
                Return CStr(ViewState("par_IndiceContratto"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceContratto") = value
        End Set

    End Property



    Protected Sub Datagrid2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles Datagrid2.SelectedIndexChanged

    End Sub
End Class
