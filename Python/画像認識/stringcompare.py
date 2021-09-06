from difflib import SequenceMatcher

s1 = '19K1131'
s2 = '19K0000'
s3 = 'ファイル'
s4 = 'type your id'
s5 = '20k1024'

s = SequenceMatcher(None, s1, s2)
print(s.ratio())