
import {FC} from 'react';
import {
 CieloSlip, CloseButton, GetnetSlip, InfoLabel, RedeSlip, SafrapaySlip, WarningContent, WarningTitle, Wrapper,
} from './slip-modal.styles';

type Props = AcquirersPage.OptionConfig.SlipModal.Props;
type EcCodeInfo = AcquirersPage.OptionConfig.SlipModal.EcCodeInfo;

const SlipModal: FC<Props> = ({
  acquirer,
  onCloseClick,
  show,
}) => {

  let ecCodeInfo: EcCodeInfo = {
    text: '',
    digits: 10,
  };

  switch (acquirer) {
    case 'getnet':
      ecCodeInfo = {
        digits: 15,
        text:`Para encontrar o seu número de EC da Getnet, localize no comprovante
          impresso um número de até 15 dígitos com as siglas EC`,
      };
      break;
    case 'safrapay':
      ecCodeInfo = {
        digits: 15,
        text:`Para encontrar o seu número de EC da SafraPay, localize no comprovante
          impresso um número de até 15 dígitos com as siglas Estab.`,
      };
      break;
    case 'macna':
      ecCodeInfo = {
        digits: 10,
        text:`Para encontrar o seu número de EC da Cielo, localize no comprovante
        impresso um número composto por 10 dígitos. Ao localizar, desconsidere os 2 primeiros e os 4 últimos conforme a imagem.`,
      };
      break;
    case 'rede':
      ecCodeInfo = {
        digits: 15,
        text:`Para encontrar o seu número de EC da Rede localize no comprovante
          impresso um número de até 9 dígitos com as siglas N. ESTAB.`,
      };
      break;
    default: throw new Error('Unknown acquirer.');
  }

  return (
    <Wrapper
      show={show}
      onBackgroundClick={onCloseClick}
    >
      <InfoLabel>
        número impresso com até {ecCodeInfo.digits} digitos
      </InfoLabel>
      {acquirer === 'macna' && <CieloSlip />}
      {acquirer === 'getnet' && <GetnetSlip />}
      {acquirer === 'rede' && <RedeSlip />}
      {acquirer === 'safrapay' && <SafrapaySlip />}
      <WarningTitle>
        Importante:
      </WarningTitle>
      <WarningContent>
        {ecCodeInfo.text}
      </WarningContent>
      <CloseButton onClick={onCloseClick}>
        fechar
      </CloseButton>
    </Wrapper>
  );
};

export default SlipModal;
