from skyfield.api import Star, load
from skyfield.data import hipparcos
import matplotlib.pyplot as plt
import numpy as np

# Load the Hipparcos catalog data into a Pandas DataFrame
with load.open(hipparcos.URL) as f:
    df = hipparcos.load_dataframe(f)

# Extract right ascension and declination
ra = df['ra_degrees'].values # in degrees
dec = df['dec_degrees'].values # in degrees

# Extract parallax
parallax = df['parallax_mas'].values # in arcseconds

# Convert the celestial coordinates and parallax data to Cartesian coordinates
x = parallax * np.cos(np.deg2rad(dec)) * np.cos(np.deg2rad(ra))
y = parallax * np.cos(np.deg2rad(dec)) * np.sin(np.deg2rad(ra))
z = parallax * np.sin(np.deg2rad(dec))

# Find all the points that do not have NaN value
valid_indices = np.where(np.logical_and(np.logical_and(~np.isnan(x), ~np.isnan(y)), ~np.isnan(z)))

# Write the coordinate data to a file (named coordinates.py)
with open('coordinates.cs', 'w') as f:
    f.write('double[][] coordinates = new double[][]{\n')
    f.write(',\n'.join([f'new double[] {{{x[i]}, {y[i]}, {z[i]}}}' for i in valid_indices[0]]))
    f.write('\n};\n')