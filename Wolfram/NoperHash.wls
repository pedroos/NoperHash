#!/usr/bin/env wolframscript
(* ::Package:: *)

Clear[NormMag,NoperFull,Noper];
NormMag=Function[x,
	x/10^MantissaExponent[x][[2]]
];
(*main function that returns the index.*)
Noper=Function[set,Module[{meanMag,xA,yA,xMag,yMag},
	meanMag=N[NormMag[Mean[set]]];
	If[meanMag==0&&StandardDeviation[set]==0,Return[0]];
	Fold[
		Function[{x,y},
			xA=Abs[x];
			yA=Abs[y];
			yMag=If[yA!=0,N[NormMag[yA]],meanMag];
			xMag=N[NormMag[xA]];
			yMag^xMag
		],
	meanMag,Reverse[set]]
]];
(*use for debugging purposes, returns the list of all intermediate values.*)
NoperFull=Function[set,Module[{meanMag,xA,yA,xMag,yMag},
	meanMag=N[NormMag[Mean[set]]];
	If[meanMag==0&&StandardDeviation[set]==0,Return[0]];
	Rest[FoldList[
		Function[{x,y},
			xA=Abs[x];
			yA=Abs[y];
			yMag=If[yA!=0,N[NormMag[yA]],meanMag];
			xMag=N[NormMag[xA]];
			yMag^xMag
		],
	meanMag,Reverse[set]]]
]];


(* ::Input:: *)
(*NoperFull[{1,499,7}]*)


(* ::Input:: *)
(*NoperFull[{100,4.99,70}]*)


Clear[sets];
sets=<||>;
sets["A"]={5,6,4.6,6.3,5,4.3,5.2};
sets["Achanged"]={5,6,4.61,6.3,5,4.3,5.2};
sets["Adisp"]=Append[sets["A"],91282712351322];
sets["B"]={0.00002,0.000035,0.000046,0.000019,0.000022,0.000016};
sets["Bdisp"]=Append[sets["B"],4718239212];
sets["C"]={515147213,515147217,515147211,515147220,515147208,515147216};
sets["Cdisp"]=Append[sets["C"],0.000001293];


(* ::Input:: *)
(*(*intermediate output*)*)
(*Print[Table[s->N[NoperFull[sets[s]]],{s,Keys[sets]}]];*)


Clear[samples];
samples={
	{1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,1429041650,1429041710,
		1429041770,1429041830},
	{24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57},
	{4054,0,237,2009,4001,4019,6368,10670,6340,1816},
	{226,0,21,156,205,240,446,519,400,127},
	{1.0146699999999999*^-7,0,0.0833333,0.633333,1.9,1.5,2.76667,5.2,3.7,0.8}
};


(* ::Input:: *)
(*(*sample data output*)*)
(*Table[Print[sample->N[Last[NoperFull[sample]]]],{sample,samples}]//InputForm;*)


Clear[TestSuite];
TestSuite=Function[{nf},
	Module[{tests},
		tests=<||>;
		
		tests["mean"]=Module[{s,s2},
			s={5,6,4.6,6.3,5,4.3,5.2};
			s2={5,5.2,4.6,6.3,5,4.3,5.2};
			Mean[s]!=Mean[s2]&&
			StandardDeviation[s]!=StandardDeviation[s2]&&
			nf[s]!=nf[s2]
		];
		
		tests["stddev"]=Module[{s,s2,s3},
			s={5,6,4.6,6.3,5,4.3,5.2};
			s2={5,5.2,5.4,6.3,5,4.3,5.2};
			s3={5,6,6.3,4.6,5,4.3,5.2};
			Mean[s]==Mean[s2]==Mean[s3]&&
			StandardDeviation[s]!=StandardDeviation[s2]&&
			StandardDeviation[s]==StandardDeviation[s3]&&
			nf[s]!=nf[s2]!=nf[s3]
		];
		
		tests["subtraction"]=Module[{s,s2,s3,s4},
			s={5,63,4.6,6.3,5,4.3,5.2};
			s2={5,63,6.3,4.6,5,4.3,5.2};
			s3={5,6.3,4.6,63,5,4.3,5.2};
			Mean[s]==Mean[s2]==Mean[s3]&&
				StandardDeviation[s]==StandardDeviation[s2]==StandardDeviation[s3]&&
				nf[s]!=nf[s2]&&
				nf[s]==nf[s3]
		];
		
		tests["asymmetric"]=Module[{s,s2,s3,s4,s5},
			s={5,63,4.6,6.2,5,4.3,5.2};
			s2={5,63,4.6,62,5,4.3,5.2};
			s3={5,6.3,46,62,5,4.3,5.2};
			s4={5,63,4.6,6.3,5,4.3,5.2};
			s5={50,6.3,46,0.63,500,0.043,.52};
			Mean[s]!=Mean[s2]!=Mean[s3]&&
				Mean[s4]!=Mean[s5]&&
			StandardDeviation[s]!=StandardDeviation[s2]!=StandardDeviation[s3]&&
				StandardDeviation[s4]!=StandardDeviation[s5]&&
			nf[s]!=nf[s2]!=nf[s3]&&
				nf[s4]!=nf[s5]
		];
		
		tests["symmetric"]=Module[{s,s2},
			s={5,62,4.6,6.2,5,4.3,5.2};
			s2={5,6.2,4.6,62,5,4.3,5.2};
			Mean[s]==Mean[s2]&&
			StandardDeviation[s]==StandardDeviation[s2]&&
			nf[s]==nf[s2]
		];
		
		tests["samples"]=Module[{s,s2,s3,s4,s5,s6,s7},
			s={1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,
				1429041650,1429041710,1429041770,1429041830};
			s2={24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57};
			s3={4054,0,237,2009,4001,4019,6368,10670,6340,1816};
			s4={226,0,21,156,205,240,446,519,400,127};
			s5={145,0,5,38,114,90,166,312,222,48};
			s6={0.000000101467,0,0.0833333,0.633333,1.9,1.5,2.76667,5.2,3.7,0.8};
			s7={0,0,0};
			nf[s]==0.37965870745378555&&
			nf[s2]==0.49196617766541745&&
			nf[s3]==0.6001859386298001&&
			nf[s4]==0.5062227481033681&&
			nf[s5]==0.5498883933485951&&
			nf[s6]==0.6145925169648433&&
			nf[s7]==0
		];
		
		tests["leadingZero"]=Module[{s,s2,s3,s4,s5},
			s={0,1429041290,1429041350,1429041410,1429041470,1429041530};
			s2={1429041290,1429041350,1429041410,1429041470,1429041530};
			s3={0,0,0,24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25};
			s4={24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25};
			nf[s]!=0&&
			nf[s3]!=0&&
			nf[s]!=nf[s2] &&
			nf[s3]!=nf[s4]
		];
		
		Table[Print[k,": ",tests[k]],{k,Keys[tests]}];
	]
];


(* ::Input:: *)
(*(*test suite execution*)*)
(*TestSuite[Noper];*)


Clear[AsymTest]
AsymTest=Function[nf,Module[{s,s2,s3,s4,s5},
	s={5,63,4.6,6.2,5,4.3,5.2};
	s2={5,63,4.6,62,5,4.3,5.2};
	s3={5,6.3,46,62,5,4.3,5.2};
	s4={5,63,4.6,6.3,5,4.3,5.2};
	s5={50,6.3,46,0.63,500,0.043,.52};
	Table[Print[set->nf[set]],{set,{s,s2,s3,s4,s5}}];
	Print[nf[s]!=nf[s2]!=nf[s3]&&
				nf[s4]!=nf[s5]]
]];


(* ::Input:: *)
(*(*asymmetrical modifications test*)*)
(*AsymTest[Noper];*)


Clear[SymTest]
SymTest=Function[nf,Module[{s,s2,s3,s4},
	s={5,62,4.6,6.2,5,4.3,5.2};
	s2={5,6.2,4.6,62,5,4.3,5.2};
	s3={5,62,6.2,4.6,5,4.3,5.2};
	Table[Print[set->nf[set]],{set,{s,s2,s3}}];
	Print[nf[s]==nf[s2]&&nf[s2]!=nf[s3]]
]];


(* ::Input:: *)
(*(*symmetric modifications test*)*)
(*SymTest[Noper];*)


(* ::Input:: *)
(*Table[ubound->RepeatedTiming[Noper[RandomSample[Range[1000],ubound]];][[1]],{ubound,{1000}}]*)


(* ::Input:: *)
(*Table[ubound->RepeatedTiming[Noper[RandomSample[Range[10000],ubound]];][[1]],{ubound,{10000}}]*)


(* ::Input:: *)
(*Table[ubound->RepeatedTiming[Noper[RandomSample[Range[50000],ubound]];][[1]],{ubound,{50000}}]*)


(* ::Input:: *)
(*Table[ubound->RepeatedTiming[Noper[RandomSample[Range[250000],ubound]];][[1]],{ubound,{250000}}]*)


(* ::Input:: *)
(*Table[ubound->RepeatedTiming[Noper[RandomSample[Range[1000000],ubound]];][[1]],{ubound,{1000000}}]*)
