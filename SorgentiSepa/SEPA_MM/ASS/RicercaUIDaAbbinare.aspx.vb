
Partial Class ASS_RicercaUIDaAbbinare
    Inherits PageSetIdMode
    Dim par As New CM.Global()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0


        If IsPostBack = False Then

            provenienza.Value = Request.QueryString("PROV")
            lIdDomanda = Request.QueryString("ID")
            HiddenField1.Value = Request.QueryString("TIPO")


            RiempiCampi()
        End If
    End Sub

    Public Property lIdDomanda() As Long
        Get
            If Not (ViewState("par_lIdDomanda") Is Nothing) Then
                Return CLng(ViewState("par_lIdDomanda"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDomanda") = value
        End Set

    End Property

    

    Private Sub RiempiCampi()

        Try
            par.caricaComboBox("SELECT ID, NOME FROM SISCOM_MI.TAB_QUARTIERI ORDER BY NOME", cmbQuartiere, "ID", "NOME", True)
            par.caricaComboBox("select COD,DESCRIZIONE from T_TIPO_ALL_ERP ORDER BY COD ASC", cmbTipo, "COD", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT DISTINCT(TIPOLOGIA_UNITA_IMMOBILIARI.COD), TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_IMMOBILIARI WHERE unita_immobiliari.cod_unita_immobiliare in (select cod_alloggio from alloggi where stato=5) and unita_immobiliari.ID_UNITA_PRINCIPALE IS NOT NULL AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND COD_TIPOLOGIA <> 'AL' ORDER BY TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE ASC", cmbPertinenze, "COD", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT COD,DESCRIZIONE from siscom_mi.TIPO_LIVELLO_PIANO order by descrizione asc", cmbPiano, "COD", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT DISTINCT indirizzi.descrizione as descriz,indirizzi.descrizione FROM siscom_mi.indirizzi,siscom_mi.unita_immobiliari WHERE indirizzi.ID=unita_immobiliari.id_indirizzo AND unita_immobiliari.cod_unita_immobiliare IN (SELECT cod_alloggio FROM ALLOGGI WHERE stato=5) AND unita_immobiliari.ID_UNITA_PRINCIPALE IS NULL union SELECT t_tipo_indirizzo.descrizione ||' '|| indirizzi.descrizione as descriz,indirizzi.descrizione FROM siscom_mi.indirizzi,t_tipo_indirizzo,alloggi WHERE alloggi.indirizzo = indirizzi.descrizione and alloggi.TIPO_INDIRIZZO=t_tipo_indirizzo.cod ORDER BY descriz ASC", cmbIndirizzo, "DESCRIZIONE", "DESCRIZ", True)


        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim RequestQueryString As String = ""
        RequestQueryString = "IDDOM=" & lIdDomanda & "&TIPO=" & HiddenField1.Value & "&Q=" & cmbQuartiere.SelectedValue & "&PROV=" & provenienza.Value & "&T=" & cmbTipo.SelectedValue & "&PRT=" & cmbPertinenze.SelectedItem.Value & "&P=" & cmbPiano.SelectedValue & "&Z=" & cmbDecentramento.SelectedValue & "&A=" & Valore01(chAscensore.Checked) & "&H=" & Valore01(chHandicap.Checked) & "&PA=" & Valore01(chPostoAuto.Checked) & "&IN=" & par.Cripta(cmbIndirizzo.SelectedItem.Text) & "&ALER=" & cmbProprieta.SelectedValue
        Response.Redirect("UnitaImmDisponibili.aspx?" & RequestQueryString)
    End Sub


    Function Valore01(ByVal Valore As Boolean) As Integer
        If Valore = True Then
            Valore01 = 1
        Else
            Valore01 = 0
        End If
    End Function
End Class
