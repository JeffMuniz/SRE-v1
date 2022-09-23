
import {
 FC, useCallback, useEffect, useMemo, useRef, useState,
} from 'react';
import {scrollToElement} from '~/utils';
import {Label, Wrapper} from './autocomplete-options.styles';

type Props = Input.AutocompleteOptions.Props;
type State = Input.AutocompleteOptions.State;

const AutocompleteOptions: FC<Props> = ({
  inputRef,
  onSelect,
  options,
  searchString,
  show,
}) => {

  const wrapperRef = useRef<HTMLDivElement>(null);

  const labelRefs = useRef<Array<HTMLLabelElement>>(
    new Array<HTMLLabelElement>(options.length).fill(null)
  );

  const filteredOptions = useMemo(() => {
    return options.filter(o => {
      if(!searchString) return true;
      else return o.toLowerCase().includes(searchString?.toLowerCase());
    }).sort();
  }, [ searchString, options ]);

  useEffect(() => {
    labelRefs.current = new Array<HTMLLabelElement>(
      filteredOptions.length
    ).fill(null);
    setState(prev => ({...prev, selected: 0}));
  },[ filteredOptions.length ]);

  const [ state, setState ] = useState<State>({
    selected: 0,
  });

  const handleMouseEnter = useCallback((selected: number) => {
    setState(prev => ({...prev, selected}));
  }, [ ]);

  const handleClick = useCallback((selected: number) => {
    onSelect({
      index: selected,
      option: filteredOptions[selected],
    });
  }, [
    onSelect,
    filteredOptions,
  ]);

  const onArrowPress = useCallback((event: KeyboardEvent) => {
    const {code} = event;
    let selected = state.selected;
    if(code === 'ArrowDown') {
      selected++;
      if(selected > filteredOptions.length - 1) {
        selected = filteredOptions.length - 1;
      }
    } else if(code === 'ArrowUp') {
      selected--;
      if(selected < 0) {
        selected = 0;
      }
    } else {
      return;
    }

    setState(prev => ({
      ...prev,
      selected,
    }));

    if(labelRefs.current[selected]) {
      scrollToElement({
        container: wrapperRef.current,
        element: labelRefs.current[selected],
      });
    }
  }, [
    state.selected,
    filteredOptions.length,
  ]);

  const onEnterPress = useCallback((event: KeyboardEvent) => {
    if(event.code !== 'Enter') return;
    onSelect({
      index: state.selected,
      option: filteredOptions[state.selected],
    });
    inputRef.current.blur();
  }, [
    filteredOptions,
    inputRef,
    onSelect,
    state.selected,
  ]);

  const onEscPress = useCallback((event: KeyboardEvent) => {
    if(event.code !== 'Escape') return;
    inputRef.current.blur();
  }, [ inputRef ]);

  useEffect(() => {
    const {current} = inputRef;
    current?.addEventListener('keyup', onArrowPress);
    current?.addEventListener('keyup', onEnterPress);
    current?.addEventListener('keyup', onEscPress);
    return () => {
      current.removeEventListener('keyup', onArrowPress);
      current.removeEventListener('keyup', onEnterPress);
      current.removeEventListener('keyup', onEscPress);
    };
  }, [
    onArrowPress,
    onEnterPress,
    onEscPress,
    inputRef,
  ]);

  return (
    <Wrapper ref={wrapperRef} show={show}>
      {filteredOptions.map((fo, index) => (
        <Label
          ref={ref => labelRefs.current[index] = ref}
          highlight={state.selected === index}
          key={`${fo}-${index}`}
          onMouseDown={() => handleClick(index)}
          onMouseEnter={() => handleMouseEnter(index)}
        >
          {fo}
        </Label>
      ))}
    </Wrapper>
  );
};

export default AutocompleteOptions;
