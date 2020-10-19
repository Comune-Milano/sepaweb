
Partial Class Contratti_SpostamentoRU_Sposta_Annulla_ERP
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            scelta = Request.QueryString("SCELTA")
            codContratto = Request.QueryString("COD")
            'If Request.QueryString("SCELTA") = "1" Then
            '    personaGiuridica = CheckGiuridica()
            '    CaricaUIERP()
            'Else
            personaGiuridica = CheckGiuridica()
            If personaGiuridica = True Then
                Response.Redirect("RiepilogoAssegnazioneG.aspx?SCELTA=" & scelta & "&COD=" & codContratto & "&CODUI=" & LBLCODUI.Value & "&IDUI=" & LBLIDUI.Value & "&TIPOUI=ERP")
            Else
                Response.Redirect("RiepilogoAssegnazione.aspx?SCELTA=" & scelta & "&COD=" & codContratto & "&CODUI=" & LBLCODUI.Value & "&IDUI=" & LBLIDUI.Value & "&TIPOUI=ERP")
            End If
            'End If
        End If
    End Sub

    Public Property personaGiuridica() As Boolean
        Get
            If Not (ViewState("par_personaGiuridica") Is Nothing) Then
                Return CLng(ViewState("par_personaGiuridica"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_personaGiuridica") = value
        End Set

    End Property

    Public Property scelta() As String
        Get
            If Not (ViewState("par_scelta") Is Nothing) Then
                Return CStr(ViewState("par_scelta"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_scelta") = value
        End Set

    End Property

    Public Property codContratto() As String
        Get
            If Not (ViewState("par_codContratto") Is Nothing) Then
                Return CStr(ViewState("par_codContratto"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_codContratto") = value
        End Set

    End Property

    Private Function CheckGiuridica() As Boolean
        Dim giuridico As Boolean = False
        Dim idContratto As Long = 0
        Dim idAnagrafica As Long = 0
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA where COD_CONTRATTO='" & codContratto & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idContratto = par.IfNull(myReader0("ID"), "0")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.SOGGETTI_CONTRATTUALI where ID_CONTRATTO=" & idContratto & " AND COD_TIPOLOGIA_OCCUPANTE ='INTE'"
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), "0")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.ANAGRAFICA where ID=" & idAnagrafica
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                If par.IfNull(myReader0("RAGIONE_SOCIALE"), "") <> "" Or par.IfNull(myReader0("PARTITA_IVA"), "") <> "" Then
                    giuridico = True
                Else
                    giuridico = False
                End If
            End If
            myReader0.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

        Return giuridico

    End Function


End Class
