import {
  FC, Fragment, useCallback, useEffect, useMemo, useState,
} from 'react';
import {observer} from 'mobx-react-lite';
import {CartDataStore} from '~/stores';
import {FeesAndTariffsPageStore} from '../fees-and-tariffs.page.store';
import {
  ColumnTitle, MErrorModal, MPageSpinner, Text, TextBold, Wrapper,
} from './table.styles';

type TableData = FeesAndTariffsPage.Table.TableData;
type State = FeesAndTariffsPage.Table.State;

const Table: FC = () => {
  const {fees} = CartDataStore.state;
  const {isLoading} = FeesAndTariffsPageStore.state;
  const [ state, setState ] = useState<State>({
    showErrorModal: false,
  });

  const getFees = useCallback(async() => {
    try {
      await FeesAndTariffsPageStore.getFees();
    } catch {
      setState(prev => ({
        ...prev,
        showErrorModal: true,
      }));
    }
  }, []);

  useEffect(() => {
    getFees();
  }, [ getFees ]);

  const tableData = useMemo<TableData>(() => [
    [{
      main: 'Ítens',
    }, {
      main: 'Condição comercial',
    }],
    ...fees.map(fee => {
      switch (fee.name) {
        case 'ADESAO':
          return [{
            title: 'Adesão',
            titleDescription: 'Cobrada no credenciamento',
          }, {
            text: `R$ ${fee.value.toLocaleString('pt-br', {minimumFractionDigits: 2})}`,
          }];
        case 'ANUIDADE':
          return [{
            title: 'Anuidade',
            titleDescription: 'Cobrada no credenciamento',
          }, {
            text: `R$ ${fee.value.toLocaleString('pt-br', {minimumFractionDigits: 2})}`,
          }];
        case 'LIQUIDACAO':
          return [{
            title: 'Liquidação',
            titleDescription: 'Cobrada a cada pagamento de liquidação',
          }, {
            text: `R$ ${fee.value.toLocaleString('pt-br', {minimumFractionDigits: 2})}`,
          }];
        case 'VA':
          return [{
            title: 'Taxa de desconto VA',
            titleDescription: 'Percentual descontado a cada transação VA',
          }, {
            text: `${fee.value}%`,
          }];
        case 'VR':
          return [{
            title: 'Taxa de desconto VR',
            titleDescription: 'Percentual descontado a cada transação VR',
          }, {
            text: `${fee.value}%`,
          }];
        default:
          return null;
      }
    }).filter(fee => fee !== null),
  ], [ fees ]);

  const handleCloseErrorModalButtonClick = useCallback(() => {
    setState(prev => ({
      ...prev,
      showErrorModal: false,
    }));
  }, [ ]);

  if (isLoading) return <MPageSpinner />;

  return (
    <>
      <Wrapper
        matrix={tableData.map((td, index) => td.map(item => (
          <Fragment key={index}>
            {item.main && (
              <ColumnTitle>
                {item.main}
              </ColumnTitle>
            )}

            {item.title && (
              <Fragment>
                <TextBold>
                  {item.title}
                </TextBold>
                <Text>
                  {item.titleDescription}
                </Text>
              </Fragment>
            )}

            {item.text && (
              <TextBold>
                {item.text}
              </TextBold>
            )}
          </Fragment>
        )))}
        highlightedColumns={[ 0 ]}
      />
      <MErrorModal
        onCloseButtonClick={handleCloseErrorModalButtonClick}
        show={state.showErrorModal}
        text='É necessário entrar em contato com a central de vendas 4004-4474 ou 0800 723 4474 para atualizar seus dados e continuar o cadastro do seu estabelecimento comercial'
      />
    </>
  );
};

export default observer(Table);
